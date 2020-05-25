import React from 'react';
import { Translate } from 'react-localize-redux';
import FormControl from '@material-ui/core/FormControl';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import { withStyles } from '@material-ui/core/styles';

import { getNestedObjectProperty } from '../../utils/nestedObjectProps';

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
    padding: '1.5rem 1rem 0',
    width: '100%',
    height: '45px',
  },
  formControl: {
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
    '& .MuiInputBase-root': {
      height: '3rem',
    },
  },
  select: {
    width: '6rem',
    top: '1.5px',
  },
};

const MultiSelect = ({ classes, obj, setObjState, label, prop }) => (
  <div className={classes.columnContainer}>
    {label ? <div className={classes.headerLabel}>{label}</div> : null}
    {obj
      ? obj.map((art, index) => {
          const selectProps = {
            value: getNestedObjectProperty(art, [prop]) || 0,
            onChange: (e) => {
              setObjState(undefined, [index, prop], e.target.value);
            },
          };
          return (
            <FormControl
              key={`MultiSelect_${label || 'label'}_${index}`}
              classes={{ root: classes.formControl }}
              variant='outlined'
            >
              <Select classes={{ root: classes.select }} {...selectProps}>
                <MenuItem value={3}>
                  <Translate id='Select.jit' />
                </MenuItem>
                <MenuItem value={4}>
                  <Translate id='Select.fast' />
                </MenuItem>
                <MenuItem value={5}>
                  <Translate id='Select.normal' />
                </MenuItem>
              </Select>
            </FormControl>
          );
        })
      : null}
  </div>
);

export default withStyles(styles)(MultiSelect);
