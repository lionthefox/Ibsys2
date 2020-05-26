import React from 'react';
import FileDownload from 'js-file-download';
import axios from 'axios';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import HomeIcon from '@material-ui/icons/Home';
import MenuBookRoundedIcon from '@material-ui/icons/MenuBookRounded';

import appIcon from '../../assets/bike.png';
import logo from '../../assets/logo.png';
import { Link } from 'react-router-dom';
import { InputLabel } from '@material-ui/core';

const styles = (theme) => ({
  root: {
    position: 'fixed',
    top: 0,
    width: '100%',
    height: '6rem',
    zIndex: 1000,
  },
  appBar: {
    height: '100%',
    backgroundColor: '#135444 !important',
    justifyContent: 'center',
    color: '#fff !important',
  },
  toolBar: {
    height: '100%',
    justifyContent: 'space-between',
  },
  title: {
    marginLeft: '1rem',
    [theme.breakpoints.down('sm')]: {
      display: 'none',
    },
  },
  appIcon: {
    height: '4rem',
    position: 'relative',
    top: '-0.3rem',
    marginRight: '1rem',
    [theme.breakpoints.down('sm')]: {
      height: '5rem',
    },
  },
  appIconContainer: {
    display: 'flex',
    alignItems: 'center',
  },
  button: {
    width: '5rem',
    height: '5rem',
    color: '#fff !important',
  },
  formControl: {
    margin: theme.spacing(1),

    '& label.Mui-focused': {
      color: '#fff',
    },
    '& .MuiInput-underline:after': {
      borderBottomColor: '#fff',
    },
    '& .MuiOutlinedInput-root': {
      '&.Mui-focused fieldset': {
        borderColor: '#fff',
      },
      '& fieldset': {
        borderColor: '#fff',
      },
    },
    '& .MuiInputBase-root': {
      height: '3rem',
      '& svg': {
        color: '#fff',
      },
    },
  },
  select: {
    width: '6rem',
    top: '1.5px',
    color: '#fff',
  },
  languageLabel: {
    color: '#fff',
  },
  link: {
    textDecoration: 'none',
  },
  logo: {
    height: '6rem',
    position: 'relative',
    left: '-1.8rem',
    [theme.breakpoints.down('md')]: {
      display: 'none',
    },
  },
  logoEn: {
    height: '6rem',
    position: 'relative',
    left: '-4rem',
    [theme.breakpoints.down('md')]: {
      display: 'none',
    },
  },
});

const Header = ({ classes, language, setLanguage, handleReset }) => {
  const downloadInstructions = () =>
    axios({
      method: 'GET',
      url: 'assets/Benutzerhandbuch.pdf',
      responseType: 'blob',
      headers: {
        'Content-Type': 'application/pdf',
      },
    }).then((response) => {
      const fileName =
        language === 'en' ? 'instructions.pdf' : 'Benutzerhandbuch.pdf';
      FileDownload(response.data, fileName);
    });
  return (
    <div className={classes.root}>
      <AppBar position='static' className={classes.appBar}>
        <Toolbar className={classes.toolBar}>
          <div className={classes.appIconContainer}>
            <img src={appIcon} alt='' className={classes.appIcon} />
            <Translate>
              {({ translate }) => (
                <Typography variant='h5' className={classes.title}>
                  {translate('Header.title')}
                </Typography>
              )}
            </Translate>
          </div>
          <div>
            <img
              src={logo}
              alt=''
              className={language === 'en' ? classes.logoEn : classes.logo}
            />
          </div>
          <div className={classes.appIconContainer}>
            <FormControl
              classes={{ root: classes.formControl }}
              variant='outlined'
            >
              <InputLabel classes={{ root: classes.languageLabel }}>
                <Translate id='Header.language' />
              </InputLabel>
              <Select
                classes={{ root: classes.select }}
                value={language}
                onChange={(e) => setLanguage(e.target.value)}
                label={<Translate id='Header.language' />}
              >
                <MenuItem value={'de'}>
                  <Translate id='Header.german' />
                </MenuItem>
                <MenuItem value={'en'}>
                  <Translate id='Header.english' />
                </MenuItem>
              </Select>
            </FormControl>
            <IconButton
              className={classes.button}
              onClick={downloadInstructions}
            >
              <MenuBookRoundedIcon fontSize='large' />
            </IconButton>
            <Link to='/input' className={classes.link}>
              <IconButton className={classes.button} onClick={handleReset}>
                <HomeIcon fontSize='large' />
              </IconButton>
            </Link>
          </div>
        </Toolbar>
      </AppBar>
    </div>
  );
};

export default withStyles(styles)(Header);
