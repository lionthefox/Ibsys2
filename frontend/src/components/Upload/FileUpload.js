import React, { useState, useRef } from 'react';
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
}) => {
  const [showErrorMessage, setShowErrorMessage] = useState(false);
  const [errorMessage, setErrorMessage] = useState();
  const dropZone = useRef(null);
  const labels = language === 'en' ? filePondLabelsEn : filePondLabels;

  return (
    <UploadContainer>
      {showErrorMessage && errorMessage ? (
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
            {errorMessage.status}: {errorMessage.statusText}
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
                setShowErrorMessage(false);
                if (response.status >= 200 && response.status < 300) {
                  load(response);
                  setResults(response.data);
                  setDisabled(false);
                } else {
                  error('Upload fehlgeschlagen');
                }
              })
              .catch(function (errorMessage) {
                console.log(errorMessage.response);
                setErrorMessage(errorMessage.response);
                setShowErrorMessage(true);
                setTimeout(() => setShowErrorMessage(false), 5000);
                error('Upload fehlgeschlagen');
              });
          },
        }}
        {...labels}
      />
    </UploadContainer>
  );
};

export default FileUpload;
