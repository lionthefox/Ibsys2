import React from 'react';
import { makeStyles } from '@material-ui/core/styles';
import { Link } from 'react-router-dom';
import Stepper from '@material-ui/core/Stepper';
import Step from '@material-ui/core/Step';
import StepLabel from '@material-ui/core/StepLabel';
import IconButton from '@material-ui/core/IconButton';
import { Translate } from 'react-localize-redux';
import Alert from '@material-ui/lab/Alert';

import ArrowBackIcon from '@material-ui/icons/ArrowBack';
import ArrowForwardIcon from '@material-ui/icons/ArrowForward';
import DoneIcon from '@material-ui/icons/Done';
import ReplayIcon from '@material-ui/icons/Replay';

const useStyles = makeStyles((theme) => ({
  root: {
    background: '#fff',
    width: '100%',
    position: 'fixed',
    bottom: '0px',
  },
  arrowContainer: {
    width: '100%',
    background: '#fff',
    display: 'flex',
    justifyContent: 'center',
    paddingBottom: '2rem',
  },
  stepIcon: {
    color: '#135444 !important',
  },
  instructions: {
    marginTop: theme.spacing(1),
    marginBottom: theme.spacing(1),
  },
  iconContainer: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  iconLabel: {
    fontSize: '1rem',
  },
  disabledIconLabel: {
    fontSize: '1rem',
    color: 'grey',
  },
  stepper: {
    width: '100%',
    display: 'flex',
    justifyContent: 'center',
  },
  horizontalSpacer: {
    width: '3rem',
  },
  verticalSpacer: { height: '10px' },
  iconButton: {
    background: '#135444',
    color: '#fff',
    '&:hover': {
      background: '#0f4336',
    },
  },
  link: {
    textDecoration: 'none',
  },
  alertContainer: {
    width: '100%',
    display: 'flex',
    justifyContent: 'center',
  },
}));

function getSteps(language) {
  return language === 'en'
    ? [
        'Upload your data',
        'Production',
        'Quantity Planning',
        'Capacity Planning',
        'Sequence Planning',
        'Order Planning',
        'Result',
      ]
    : [
        'Simulationsdaten hochladen',
        'Produktion',
        'Mengenplanung',
        'Kapazitätsplanung',
        'Reihenfolgenplanung',
        'Bestellmengenplanung',
        'Ergebnis',
      ];
}

const HorizontalStepper = ({
  paths,
  language,
  activeStep,
  handleNext,
  handleBack,
  handleReset,
  showError,
  errorMessageId,
  errorMessage,
}) => {
  const classes = useStyles();

  const steps = getSteps(language);

  return (
    <div className={classes.root}>
      {showError ? (
        <div
          style={{ width: '100%', display: 'flex', justifyContent: 'center' }}
        >
          <div
            className='cssanimation sequence fadeInBottom'
            style={{
              width: '80%',
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
        </div>
      ) : null}
      <Stepper activeStep={activeStep} alternativeLabel>
        {steps.map((label) => (
          <Step key={label}>
            <StepLabel
              StepIconProps={{
                classes: {
                  active: classes.stepIcon,
                  completed: classes.stepIcon,
                },
              }}
            >
              {label}
            </StepLabel>
          </Step>
        ))}
      </Stepper>
      <div className={classes.arrowContainer}>
        {activeStep === steps.length - 1 ? (
          <div className={classes.iconContainer}>
            <Link to={paths[0]} className={classes.link}>
              <IconButton className={classes.iconButton} onClick={handleReset}>
                <ReplayIcon fontSize='large' />
              </IconButton>
            </Link>
            <span className={classes.verticalSpacer} />
            <span className={classes.iconLabel}>
              <Translate id='Stepper.restart' />
            </span>
          </div>
        ) : (
          <div style={{ width: '100%' }}>
            <div className={classes.stepper}>
              <div className={classes.iconContainer}>
                <IconButton
                  className={classes.iconButton}
                  disabled={activeStep === 0}
                  onClick={handleBack}
                >
                  <ArrowBackIcon fontSize='large' />
                </IconButton>
                <span className={classes.verticalSpacer} />
                <span
                  className={
                    activeStep === 0
                      ? classes.disabledIconLabel
                      : classes.iconLabel
                  }
                >
                  <Translate id='Stepper.back' />
                </span>
              </div>
              <span className={classes.horizontalSpacer} />
              <div className={classes.iconContainer}>
                <IconButton className={classes.iconButton} onClick={handleNext}>
                  {activeStep === steps.length - 2 ? (
                    <DoneIcon fontSize='large' />
                  ) : (
                    <ArrowForwardIcon fontSize='large' />
                  )}
                </IconButton>
                <span className={classes.verticalSpacer} />
                {activeStep === steps.length - 2 ? (
                  <span className={classes.iconLabel}>
                    <Translate id='Stepper.finish' />
                  </span>
                ) : (
                  <span className={classes.iconLabel}>
                    <Translate id='Stepper.next' />
                  </span>
                )}
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default HorizontalStepper;
