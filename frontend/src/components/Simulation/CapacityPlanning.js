import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';
import useMediaQuery from '@material-ui/core/useMediaQuery';

import Form from '../ui_components/Form';
import Text from '../ui_components/Text';
import { Paper } from '@material-ui/core';
import clsx from 'clsx';

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
  },
  paper: {
    marginTop: '1rem',
    height: 'calc(100vh - 390px)',
    overflow: 'auto',
  },
  form: {
    display: 'flex',
    width: '90vw',
  },
  centered: {
    justifyContent: 'center',
  },
};

const CapacityPlanning = ({ classes, capacityPlan, changeCapacityPlan }) => {
  const centered = useMediaQuery('(min-width:1500px)');

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
                  small={artKey === 'anzSchicht'}
                  medium={artKey === 'ubermin'}
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
                stickyIDs={artKey === 'arbeitsplatzId'}
              />
            )
      );
    }

    return <div className={classes.root}>{capacityPlan ? elements : null}</div>;
  };
  return (
    <Paper classes={{ root: classes.paper }} elevation={3}>
      <div className={classes.wrapper}>
        <form
          className={clsx(classes.form, { [classes.centered]: centered })}
          noValidate
          autoComplete='off'
        >
          {getComponents()}
        </form>
      </div>
    </Paper>
  );
};

export default withStyles(styles)(CapacityPlanning);
