import React from 'react';
import { Link } from 'react-router-dom';
import { makeStyles } from '@material-ui/core/styles';
import { Button } from '@material-ui/core';
import { Translate } from 'react-localize-redux';

import FileUpload from './FileUpload';

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
  link: {
    textDecoration: 'none',
  },
}));

const Input = ({ language }) => {
  const classes = useStyles();

  return (
    <div className='cssanimation sequence fadeInBottom'>
      <FileUpload
        language={language}
        multipleFiles={false}
        url='/simulation/results-input'
      />
      <div className={classes.container}>
        <Link to='/forecast' className={classes.link}>
          <Button variant='contained' classes={{ root: classes.buttonRoot }}>
            <Translate id='Input.start' />
          </Button>
        </Link>
      </div>
    </div>
  );
};

export default Input;
