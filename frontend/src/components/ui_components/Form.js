import React from 'react';
import TextField from '@material-ui/core/TextField';
import { withStyles } from '@material-ui/core/styles';
import { getNestedObjectProperty } from '../../utils/nestedObjectProps';
import { getFloatValue } from '../../utils/getValue';

const Input = withStyles({
  root: {
    marginBottom: '10px',
    width: '10rem',

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
  },
})(TextField);

const Form = ({ classes, obj, setObjState, label, values }) => {
  const inputProps = {
    type: 'number',
    variant: 'outlined',
  };

  const getInputValue = (val) => String(val).replace('^0+', '');

  return (
    <div className={classes.columnContainer}>
      {label ? <div className={classes.headerLabel}>{label}</div> : null}
      {values.map((val, index) => (
        <Input
          key={`Input_${label || 'label'}_${index}`}
          {...inputProps}
          value={getInputValue(getNestedObjectProperty(obj, val)) || 0}
          onChange={(e) => {
            setObjState(val, getFloatValue(e.target.value));
          }}
        />
      ))}
    </div>
  );
};

export default Form;
