import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';
import Popover from '@material-ui/core/Popover';
import { TextField, Button } from '@material-ui/core';

import artNumbers from '../../assets/artNumbers';
import { getInputValue } from '../../utils/getValue';

const styles = {
  splitTextButton: {
    textTransform: 'none',
    fontSize: '18px',
    padding: '5px 15px',
    background: '#135444',
    color: '#fff',
    '&:hover': {
      background: '#0f4336',
    },
  },
  splitTextButtonDisabled: {
    background: 'grey',
    color: '#fff !important',
  },
  popoverPaper: {
    boxShadow: '1px 2px 4px 0px rgba(194,194,194,0.5)',
  },
  textFieldError: {
    '& label.Mui-focused': {
      color: '#f44336',
    },
    '& .MuiOutlinedInput-root': {
      '&.Mui-focused fieldset': {
        borderColor: '#f44336',
      },
    },
  },
};

const inputStyleRoot = {
  margin: '1rem',

  '& label.Mui-focused': {
    color: '#135444',
  },
  '& .MuiInput-underline:after': {
    borderBottomColor: '#135444',
  },
  '& .MuiOutlinedInput-root': {
    '&.Mui-focused fieldset': {
      borderColor: '#135444',
    },
  },
};

const MediumInput = withStyles({
  root: {
    ...inputStyleRoot,
    width: '8rem',
    top: '1.5px',
    '& .MuiInputBase-root': {
      height: '3rem',
    },
  },
})(TextField);

const OverflowPopover = ({
  classes,
  val,
  open,
  anchorEl,
  closePopover,
  splittingValue1,
  splittingValue2,
  setSplittingValue1,
  setSplittingValue2,
  submitSplittingValues,
}) => {
  const error1 = splittingValue1 < 10;
  const error2 = splittingValue2 < 10;
  const maxValue = val ? val.produktion - 10 : undefined;
  const inputProps = {
    type: 'number',
    variant: 'outlined',
    InputProps: {
      inputProps: { min: 10, max: maxValue, step: 10 },
    },
  };

  const setNewSplittingValues = (newVal, input1) => {
    let input = parseInt(newVal.replace('-', ''));
    if (input > maxValue) {
      input = maxValue;
    }
    if (input1) {
      setSplittingValue1(input);
      setSplittingValue2(val.produktion - input);
    } else {
      setSplittingValue2(input);
      setSplittingValue1(val.produktion - input);
    }
  };
  return val ? (
    <Popover
      classes={{ paper: classes.popoverPaper }}
      open={open}
      anchorEl={anchorEl}
      onClose={closePopover}
      anchorOrigin={{
        vertical: 'bottom',
        horizontal: 'center',
      }}
      transformOrigin={{
        vertical: 'top',
        horizontal: 'center',
      }}
    >
      <div
        style={{
          display: 'flex',
          width: '100%',
          justifyContent: 'center',
          marginTop: '0.6rem',
          fontSize: '18px',
          color: '#135444',
        }}
      >
        {val ? artNumbers[val.articleId - 1] : null}
      </div>
      <div>
        <MediumInput
          {...inputProps}
          classes={{ root: error1 ? classes.textFieldError : null }}
          error={error1}
          label={error1 ? <Translate id='Popover.invalidNumber' /> : null}
          helperText={error1 ? <Translate id='Popover.helperText' /> : null}
          value={getInputValue(splittingValue1) || 0}
          onChange={(e) => setNewSplittingValues(e.target.value, true)}
        />
        <MediumInput
          {...inputProps}
          classes={{ root: error2 ? classes.textFieldError : null }}
          error={error2}
          label={error2 ? <Translate id='Popover.invalidNumber' /> : null}
          helperText={error2 ? <Translate id='Popover.helperText' /> : null}
          value={getInputValue(splittingValue2) || 0}
          onChange={(e) => setNewSplittingValues(e.target.value, false)}
        />
      </div>
      <div
        style={{
          display: 'flex',
          width: '100%',
          justifyContent: 'center',
          marginBottom: '0.6rem',
        }}
      >
        <Button
          disabled={
            error1 || error2 || isNaN(splittingValue1) || isNaN(splittingValue2)
          }
          onClick={() => {
            submitSplittingValues();
            closePopover();
          }}
          classes={{
            root: classes.splitTextButton,
            disabled: classes.splitTextButtonDisabled,
          }}
        >
          <Translate id='SequencePlanning.splitting' />
        </Button>
      </div>
    </Popover>
  ) : null;
};

export default withStyles(styles)(OverflowPopover);
