import React from 'react';
import TextField from '@material-ui/core/TextField';
import { withStyles } from '@material-ui/core/styles';
import { getNestedObjectProperty } from '../../utils/nestedObjectProps';
import { getValue, getInputValue } from '../../utils/getValue';

const styles = {
  columnContainer: {
    display: 'flex',
    flexDirection: 'column',
    marginRight: '1rem',
    alignItems: 'center',
  },
  headerLabel: {
    textAlign: 'center',
    fontSize: '20px',
    color: '#135444',
    background: '#fff',
    position: 'sticky',
    top: '0rem',
    zIndex: 100,
    padding: '1.5rem 1rem',
    width: '100%',
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

const SmallInput = withStyles({
  root: {
    ...inputStyleRoot,
    top: '1.5px',
    width: '6rem',
    '& .MuiInputBase-root': {
      height: '3rem',
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
  medium,
  small,
  decimal,
  maxValue,
}) => {
  const inputBaseProps = {
    type: 'number',
    variant: 'outlined',
    InputProps: { inputProps: { min: 0, max: maxValue || undefined } },
  };

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
                setObjState(
                  undefined,
                  val,
                  getValue(e.target.value, decimal, maxValue)
                );
              },
            };
            return medium ? (
              <MediumInput {...inputProps} />
            ) : small ? (
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
                onChange: product
                  ? (e) =>
                      setObjState(
                        product,
                        [index, prop],
                        getValue(e.target.value, decimal, maxValue)
                      )
                  : (e) => {
                      if (
                        prop === 'anzSchicht' &&
                        getValue(e.target.value, decimal, maxValue) === 3
                      ) {
                        setObjState(undefined, [index, 'ubermin'], 0);
                      }
                      setObjState(
                        undefined,
                        [index, prop],
                        getValue(e.target.value, decimal, maxValue)
                      );
                    },
                disabled: prop === 'ubermin' && art['anzSchicht'] === 3,
              };
              return medium ? (
                <MediumInput {...inputProps} />
              ) : small ? (
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
