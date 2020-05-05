import React, { useState, useRef } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import { FilePond } from 'react-filepond';
import axios from 'axios';

import simulationInput from '../../assets/examples/simulation_input';
import xmlToJson from './XMLtoJSONConverter';

const useStyles = makeStyles(() => ({
  uploadContainer: {
    height: '42.95vh',
    width: '100vw',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    overflow: 'hidden',
  },
  fileImportContainer: {
    minHeight: '24vh',
    width: '100vw',
    display: 'flex',
    justifyContent: 'center',
  },
  fileImport: {
    width: '40vw',
  },
  importTypography: {
    textAlign: 'center !important',
  },
  formatTypography: {
    marginLeft: '1px',
    color: 'gray',
  },
}));

const filePondLabels = {
  labelIdle:
    'Ziehen Sie Ihre Datei hierhin oder <span class="filepond--label-action"> durchsuchen </span> Sie Ihren Computer',
  labelInvalidField: 'Sie haben ungültige Dateien hinzugefügt',
  labelFileWaitingForSize: 'Größe berechnen',
  labelFileSizeNotAvailable: 'Größe konnte nicht berechnet werden',
  labelFileLoading: 'Lädt',
  labelFileLoadError: 'Fehler beim Laden',
  labelFileProcessing: 'Lädt hoch',
  labelFileProcessingComplete: 'Hochgeladen',
  labelFileProcessingAborted: 'Hochladen abgebrochen',
  labelFileProcessingError: 'Hochladen fehlgeschlagen',
  labelFileProcessingRevertError: 'Rückgängig machen fehlgeschlagen',
  labelFileRemoveError: 'Entfernen fehlgeschlagen',
  labelTapToCancel: 'Abbrechen',
  labelTapToRetry: 'Erneut versuchen',
  labelTapToUndo: 'Datei entfernen',
  labelButtonRemoveItem: 'Entfernen',
  labelButtonAbortItemLoad: 'Abbrechen',
  labelButtonRetryItemLoad: 'Erneut versuchen',
  labelButtonAbortItemProcessing: 'Wird abgebrochen',
  labelButtonUndoItemProcessing: 'Wird rückgängig gemacht',
  labelButtonRetryItemProcessing: 'Wird erneut versucht',
  labelButtonProcessItem: 'Hochladen',
  labelFileTypeNotAllowed: 'Datentyp nicht erlaubt',
  fileValidateTypeLabelExpectedTypes: 'nur .xml-Dateien erlaubt',
};

const UploadContainer = ({ children }) => {
  const classes = useStyles();
  const uploadString = 'Laden Sie hier Ihre Simulationsdatei hoch';
  return (
    <div className='cssanimation sequence fadeInBottom'>
      <div className={classes.uploadContainer}>
        <Typography variant='h4' className={classes.importTypography}>
          {uploadString}
        </Typography>
        <Typography variant='h5' className={classes.formatTypography}>
          Format: .xml
        </Typography>
      </div>
      <div className={classes.fileImportContainer}>
        <div className={classes.fileImport}>{children}</div>
      </div>
    </div>
  );
};

const FileUpload = ({ multipleFiles, url }) => {
  const [showErrorMessage, setShowErrorMessage] = useState(false);
  const [errorMessage, setErrorMessage] = useState();
  const dropZone = useRef(null);

  return (
    <UploadContainer>
      {showErrorMessage && errorMessage ? (
        <div
          style={{
            width: '100%',
            textAlign: 'center',
            fontSize: '1rem',
            color: 'red',
            paddingBottom: '1rem',
          }}
        >
          {errorMessage.status}: {errorMessage.statusText}
        </div>
      ) : null}
      <FilePond
        ref={dropZone}
        allowMultiple={multipleFiles}
        acceptedFileTypes={['text/xml']}
        allowReorder={true}
        instantUpload={true}
        dropOnPage={true}
        maxParallelUploads={1}
        server={{
          timeout: 5000,
          fetch: null,
          revert: null,
          process: (fieldName, file, metadata, load, error, progress) => {
            const encodedXML = dropZone.current
              .getFile()
              .getFileEncodeBase64String();
            const decodedXML = atob(encodedXML);
            const parsedXML = new DOMParser().parseFromString(
              decodedXML,
              'text/xml'
            );
            const jsonObj = xmlToJson(parsedXML);
            /* jsonObj.results.forecast = {
              periode1: {
                produkt1: 5,
                produkt2: 5,
                produkt3: 5,
              },
              periode2: {
                produkt1: 5,
                produkt2: 5,
                produkt3: 5,
              },
              periode3: {
                produkt1: 5,
                produkt2: 5,
                produkt3: 5,
              },
              periode4: {
                produkt1: 5,
                produkt2: 5,
                produkt3: 5,
              },
            };
            jsonObj.results.vertriebswunsch = {
              produkt1: 5,
              produkt2: 5,
              produkt3: 5,
              direktverkauf: {
                produkt1: {
                  menge: 5,
                  preis: 5.5,
                  konventionalstrafe: 0.0,
                },
                produkt2: {
                  menge: 5,
                  preis: 5.5,
                  konventionalstrafe: 0.0,
                },
                produkt3: {
                  menge: 5,
                  preis: 5.5,
                  konventionalstrafe: 0.0,
                },
              },
            }; */

            axios({
              url: url,
              method: 'POST',
              headers: { 'Content-Type': 'application/json' },
              data: jsonObj,
              onUploadProgress: (e) => {
                progress(e.lengthComputable, e.loaded, e.total);
              },
            })
              .then(function (response) {
                console.log(response);
                setShowErrorMessage(false);
                if (response.status >= 200 && response.status < 300) {
                  load(response.responseText);
                } else {
                  error('Upload fehlgeschlagen');
                }
              })
              .catch(function (errorMessage) {
                console.log(errorMessage.response);
                setErrorMessage(errorMessage.response);
                setShowErrorMessage(true);
                error('Upload fehlgeschlagen');
              });
          },
        }}
        {...filePondLabels}
      />
    </UploadContainer>
  );
};

export default FileUpload;
