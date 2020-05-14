import React, { useRef } from 'react';
import { makeStyles } from '@material-ui/core/styles';
import Typography from '@material-ui/core/Typography';
import { FilePond } from 'react-filepond';
import { Translate } from 'react-localize-redux';
import Alert from '@material-ui/lab/Alert';

import axios from 'axios';

import {
  filePondLabels,
  filePondLabelsEn,
} from '../../translations/filePondLabels';

const useStyles = makeStyles(() => ({
  uploadContainer: {
    width: '100%',
    marginTop: '8rem',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    overflow: 'hidden',
  },
  fileImportContainer: {
    height: '18vh',
    width: '100%',
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
    marginBottom: '5rem',
  },
}));

const UploadContainer = ({ children }) => {
  const classes = useStyles();
  return (
    <>
      <div className={classes.uploadContainer}>
        <Typography variant='h4' className={classes.importTypography}>
          <Translate id='FileUpload.uploadString' />
        </Typography>
        <Typography variant='h5' className={classes.formatTypography}>
          <Translate id='FileUpload.format'>Format: .xml</Translate>
        </Typography>
      </div>
      <div className={classes.fileImportContainer}>
        <div className={classes.fileImport}>{children}</div>
      </div>
    </>
  );
};

const FileUpload = ({
  language,
  multipleFiles,
  url,
  setResults,
  setDisabled,
  showError,
  errorMessageId,
  errorMessage,
  setError,
}) => {
  const dropZone = useRef(null);
  const labels = language === 'en' ? filePondLabelsEn : filePondLabels;

  return (
    <UploadContainer>
      {showError ? (
        <div
          className='cssanimation sequence fadeInBottom'
          style={{
            width: '100%',
            textAlign: 'center',
            fontSize: '1rem',
            color: 'red',
            paddingBottom: '1rem',
          }}
        >
          <Alert severity='error'>
            <Translate id={errorMessageId} />{' '}
            {errorMessage
              ? ` (${errorMessage.status}: ${errorMessage.statusText})`
              : null}
          </Alert>
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

            axios({
              url: url,
              method: 'POST',
              headers: { 'Content-Type': 'application/xml' },
              data: decodedXML,
              onUploadProgress: (e) => {
                progress(e.lengthComputable, e.loaded, e.total);
              },
            })
              .then(function (response) {
                console.log(response);
                if (response.status >= 200 && response.status < 300) {
                  load(response);
                  setError(false, undefined, undefined);
                  setResults(response.data);
                  setDisabled(false);
                } else {
                  error('Upload fehlgeschlagen');
                  setError(true, 'Main.error.serverError', response);
                  setTimeout(
                    () => setError(false, undefined, undefined),
                    10000
                  );
                }
              })
              .catch(function (errorMessage) {
                error('Upload fehlgeschlagen');
                const response = errorMessage.response;
                let translateId = 'Main.error.serverError';
                if (response && response.status >= 500) {
                  translateId = 'Main.error.uploadError';
                }
                setError(true, translateId, response);
                setTimeout(() => setError(false, undefined, undefined), 10000);
              });
          },
        }}
        {...labels}
      />
    </UploadContainer>
  );
};

export default FileUpload;
