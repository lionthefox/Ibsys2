import React from 'react';
import { makeStyles, withStyles } from '@material-ui/core/styles';
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

const useStyles = makeStyles((theme) => ({
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
}));

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

const Header = ({ language, setLanguage }) => {
  const classes = useStyles();

  return (
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
                <option value='Deutsch'>Deutsch</option>
                <option value='English'>English</option>
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
};

export default Header;
