import React from 'react';
import { Button, withStyles } from '@material-ui/core';
import { Translate } from 'react-localize-redux';

const styles = {
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
};

const Result = ({ classes, simulationOutput }) => {
  const downloadOutput = () => {
    const pom = document.createElement('a');
    const filename = 'output.xml';
    const blob = new Blob([simulationOutput], { type: 'text/plain' });

    pom.setAttribute('href', window.URL.createObjectURL(blob));
    pom.setAttribute('download', filename);

    pom.dataset.downloadurl = ['text/plain', pom.download, pom.href].join(':');
    pom.draggable = true;
    pom.classList.add('dragout');

    pom.click();
  };
  return (
    <div className={classes.container}>
      <Button
        variant='contained'
        classes={{ root: classes.buttonRoot }}
        onClick={() => downloadOutput()}
      >
        <Translate id='Result.download' />
      </Button>
    </div>
  );
};

export default withStyles(styles)(Result);
