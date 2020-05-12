import React, { useState } from 'react';
import { Link } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import Tooltip from '@material-ui/core/Tooltip';
import { Button } from '@material-ui/core';
import { Translate } from 'react-localize-redux';

import FileUpload from './FileUpload';
import Alert from '@material-ui/lab/Alert';

const useStyles = makeStyles(() => ({
  container: {
    width: '100%',
    position: 'relative',
    display: 'flex',
    justifyContent: 'center',
  },
  buttonRoot: {
    color: '#fff',
    background: '#135444',
    borderRadius: '15px',
    padding: '2rem',
    fontSize: '18px',
    '&:hover': {
      background: '#0f4336',
    },
  },
  link: {
    textDecoration: 'none',
  },
  tooltip: {
    background: '#fff',
  },
}));

const Input = ({ language, setLastPeriodResults, handleNext }) => {
  const classes = useStyles();
  const [disabled, setDisabled] = useState(true);
  const [results, setResults] = useState(undefined);

  return (
    <>
      <FileUpload
        language={language}
        multipleFiles={false}
        url='/api/simulation/results-input'
        setResults={setResults}
        setDisabled={setDisabled}
      />
      <div className={classes.container}>
        {disabled ? (
          <Tooltip
            classes={{ tooltip: classes.tooltip }}
            title={
              <Alert severity='error'>
                <Translate id='Input.alert' />
              </Alert>
            }
            enterDelay={500}
            leaveDelay={200}
          >
            <span>
              <Button
                disabled
                variant='contained'
                classes={{ root: classes.buttonRoot }}
              >
                <Translate id='Input.start' />
              </Button>
            </span>
          </Tooltip>
        ) : (
          <Link to='/production' className={classes.link}>
            <Button
              variant='contained'
              classes={{ root: classes.buttonRoot }}
              onClick={() => {
                setLastPeriodResults(results);
                handleNext();
              }}
            >
              <Translate id='Input.start' />
            </Button>
          </Link>
        )}
      </div>
    </>
  );
};

export default Input;
