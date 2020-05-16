import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';

import Form from '../ui_components/Form';
import Text from '../ui_components/Text';

const styles = {
  wrapper: {
    width: '100%',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  root: {
    display: 'flex',
    overflowY: 'scroll',
    justifyContent: 'space-around',
    marginTop: '2rem',
  },
};

const CapacityPlanning = ({ classes, capacityPlan, changeCapacityPlan }) => {
  console.log(capacityPlan);
  const getComponent = () => {
    const elements = [];
    const formProps = {
      obj: capacityPlan || undefined,
      setObjState: changeCapacityPlan,
    };

    if (capacityPlan) {
      let keys = [];
      Object.keys(capacityPlan[0]).map((key) => keys.push(key));
      keys = keys.filter((val) => {
        if (
          val === 'ruestzeitProduktion' ||
          val === 'kapafBearbeitung' ||
          val === 'ruestzeitBearbeitung' ||
          val === 'kapaWarteschlange' ||
          val === 'ruestzeitWarteschlange'
        )
          return false;
      });

      keys.map((artKey) =>
        artKey === 'ubermin' || 'anzSchicht'
          ? elements.push(
              <Form
                {...formProps}
                key={`capacity_planning_form_${artKey}`}
                label={<Translate id={`CapacityPlanning.${artKey}`} />}
                prop={artKey}
                small
              />
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
    <div className={classes.wrapper}>
      <form noValidate autoComplete='off'>
        {getComponent()}
      </form>
    </div>
  );
};

export default withStyles(styles)(CapacityPlanning);
