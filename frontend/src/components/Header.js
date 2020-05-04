import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import AppBar from '@material-ui/core/AppBar';
import Toolbar from '@material-ui/core/Toolbar';
import Typography from '@material-ui/core/Typography';
import IconButton from '@material-ui/core/IconButton';
import SettingsIcon from '@material-ui/icons/Settings';

const useStyles = makeStyles(() => ({
  root: {
    height: '6rem',
  },
  appBar: {
    height: '100%',
    backgroundColor: '#fff !important',
    justifyContent: 'center',
  },
  toolBar: {
    height: '100%',
    justifyContent: 'space-between',
  },
  title: {
    marginLeft: '1rem',
    color: 'rgb(84, 84, 85)',
  },
  appIconContainer: {
    display: 'flex',
    alignItems: 'center',
  },
  settingsButton: {
    width: '5rem',
    height: '5rem',
  },
}));

const Header = () => {
  const classes = useStyles();

  return (
    <div className={classes.root}>
      <AppBar position='static' className={classes.appBar}>
        <Toolbar className={classes.toolBar}>
          <div className={classes.appIconContainer}>
            <Typography variant='h3' className={classes.title}>
              IBSYS2 SCM-Tool
            </Typography>
          </div>
          <IconButton className={classes.settingsButton}>
            <SettingsIcon />
          </IconButton>
        </Toolbar>
      </AppBar>
    </div>
  );
};

export default Header;
