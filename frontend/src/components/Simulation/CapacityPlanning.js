import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';

import Form from '../ui_components/Form';
import Text from '../ui_components/Text';
import { Paper } from '@material-ui/core';

const styles = {
  wrapper: {
    width: '100%',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    paddingBottom: '1rem',
  },
  root: {
    display: 'flex',
    justifyContent: 'space-around',
    marginTop: '2rem',
  },
};

const CapacityPlanning = ({ classes, capacityPlan, changeCapacityPlan }) => {
  const getComponents = () => {
    const elements = [];
    const formProps = {
      obj: capacityPlan || undefined,
      setObjState: changeCapacityPlan,
    };

    if (capacityPlan) {
      let keys = [];
      Object.keys(capacityPlan[0]).map((key) => keys.push(key));

      keys.map((artKey) =>
        artKey === 'ubermin' || artKey === 'anzSchicht'
          ? elements.push(
              <div
                key={`capacity_planning_form_${artKey}_wrapper`}
                style={{ marginRight: '1rem' }}
              >
                <Form
                  {...formProps}
                  maxValue={artKey === 'ubermin' ? 240 : 3}
                  key={`capacity_planning_form_${artKey}`}
                  label={<Translate id={`CapacityPlanning.${artKey}`} />}
                  prop={artKey}
                  small
                />
              </div>
            )
          : elements.push(
              <Text
                key={`capacity_planning_text_${artKey}`}
                obj={capacityPlan}
                idProp='arbeitsplatzId'
                label={<Translate id={`CapacityPlanning.${artKey}`} />}
                prop={artKey}
              />
            )
      );
    }

    return <div className={classes.root}>{capacityPlan ? elements : null}</div>;
  };
  return (
    <Paper elevation={3}>
      <div className={classes.wrapper}>
        <form noValidate autoComplete='off'>
          {getComponents()}
        </form>
      </div>
    </Paper>
  );
};

export default withStyles(styles)(CapacityPlanning);
