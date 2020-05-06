import React, { Component } from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate, withLocalize } from 'react-localize-redux';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import FormControl from '@material-ui/core/FormControl';
import NativeSelect from '@material-ui/core/NativeSelect';
import InputBase from '@material-ui/core/InputBase';
import InputLabel from '@material-ui/core/InputLabel';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import HomeIcon from '@material-ui/icons/Home';

import appIcon from '../assets/bike.png';

const styles = (theme) => ({
  root: {
    height: '8rem',
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
    height: '8rem',
    position: 'relative',
    top: '-0.3rem',
    [theme.breakpoints.down('sm')]: {
      height: '5rem',
    },
  },
  appIconContainer: {
    display: 'flex',
    alignItems: 'center',
  },
  homeButton: {
    width: '5rem',
    height: '5rem',
    color: '#fff !important',
  },
  margin: {
    margin: theme.spacing(1),
  },
  inputLabel: {
    color: '#fff !important',
  },
  selectIcon: {
    color: '#fff',
    position: 'relative',
    top: '-0.75rem',
    left: '-2rem',
  },
  nativeSelect: {
    marginBottom: '1.5rem',
  },
});

const BootstrapInput = withStyles((theme) => ({
  root: {
    'label + &': {
      marginTop: theme.spacing(3),
    },
  },
  input: {
    borderRadius: 4,
    width: '8rem',
    position: 'relative',
    color: '#fff',
    border: '1px solid #fff',
    fontSize: 16,
    padding: '10px 26px 10px 12px',
    '&:focus': {
      borderRadius: 4,
    },
  },
}))(InputBase);

const Header = ({ classes, language, setLanguage }) => (
  <div className={classes.root}>
    <AppBar position='static' className={classes.appBar}>
      <Toolbar className={classes.toolBar}>
        <div className={classes.appIconContainer}>
          <Typography variant='h5' className={classes.title}>
            Produktionsplanungstool
          </Typography>
        </div>
        <img src={appIcon} alt='' className={classes.appIcon} />
        <div className={classes.appIconContainer}>
          <FormControl className={classes.margin}>
            <InputLabel classes={{ root: classes.inputLabel }}>
              Language
            </InputLabel>
            <NativeSelect
              classes={{
                root: classes.nativeSelect,
                icon: classes.selectIcon,
              }}
              id='demo-customized-select-native'
              value={language}
              onChange={(e) => setLanguage(e.target.value)}
              input={<BootstrapInput />}
            >
              <Translate>
                {({ translate }) => (
                  <option value='de'>{translate('german')}</option>
                )}
              </Translate>
              <Translate>
                {({ translate }) => (
                  <option value='en'>{translate('english')}</option>
                )}
              </Translate>
            </NativeSelect>
          </FormControl>
          <IconButton className={classes.homeButton}>
            <HomeIcon fontSize='large' />
          </IconButton>
        </div>
      </Toolbar>
    </AppBar>
  </div>
);

export default withStyles(styles)(Header);
