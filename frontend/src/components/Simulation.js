import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Button } from '@material-ui/core';
import { Translate } from 'react-localize-redux';

import FileUpload from './Upload/FileUpload';

const useStyles = makeStyles(() => ({
  container: {
    width: '100%',
    position: 'relative',
    top: '-5rem',
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
}));

const Simulation = ({ language }) => {
  const classes = useStyles();

  return (
    <div className='cssanimation sequence fadeInBottom'>
      <FileUpload
        language={language}
        multipleFiles={false}
        url='/simulation/results-input'
      />
      <div className={classes.container}>
        <Button variant='contained' classes={{ root: classes.buttonRoot }}>
          <Translate id='Simulation.start' />
        </Button>
      </div>
    </div>
  );
};

export default Simulation;
