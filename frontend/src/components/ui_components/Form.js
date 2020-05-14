import React from 'react';
import TextField from '@material-ui/core/TextField';
import { withStyles } from '@material-ui/core/styles';
import { getNestedObjectProperty } from '../../utils/nestedObjectProps';
import { getFloatValue } from '../../utils/getValue';

const styles = {
  columnContainer: {
    display: 'flex',
    flexDirection: 'column',
    marginRight: '1rem',
  },
  headerLabel: {
    marginBottom: '1.5rem',
    textAlign: 'center',
    fontSize: '18px',
  },
};

const inputStyleRoot = {
  marginBottom: '10px',

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

const SmallInput = withStyles({
  root: {
    ...inputStyleRoot,
    width: '6rem',
    '& .MuiInputBase-root': {
      height: '2.5rem',
      fontSize: '15px',
    },
  },
})(TextField);

const Input = withStyles({
  root: {
    ...inputStyleRoot,
    width: '10rem',
  },
})(TextField);

const Form = ({
  classes,
  obj,
  setObjState,
  label,
  values,
  prop,
  product,
  small,
}) => {
  const inputBaseProps = {
    type: 'number',
    variant: 'outlined',
  };

  const getInputValue = (val) => String(val).replace('^0+', '');

  return (
    <div className={classes.columnContainer}>
      {label ? <div className={classes.headerLabel}>{label}</div> : null}
      {values
        ? values.map((val, index) => {
            const inputProps = {
              ...inputBaseProps,
              key: `Input_${label || 'label'}_${index}`,
              value: getInputValue(getNestedObjectProperty(obj, val)) || 0,
              onChange: (e) => {
                setObjState(undefined, val, getFloatValue(e.target.value));
              },
            };
            return small ? (
              <SmallInput {...inputProps} />
            ) : (
              <Input {...inputProps} />
            );
          })
        : prop
        ? obj
          ? obj.map((art, index) => {
              const inputProps = {
                ...inputBaseProps,
                key: `Input_${label || 'label'}_${index}`,
                value: getInputValue(getNestedObjectProperty(art, [prop])) || 0,
                onChange: (e) => {
                  setObjState(
                    product,
                    [index, prop],
                    getFloatValue(e.target.value)
                  );
                },
              };
              return small ? (
                <SmallInput {...inputProps} />
              ) : (
                <Input {...inputProps} />
              );
            })
          : null
        : null}
    </div>
  );
};

export default withStyles(styles)(Form);
