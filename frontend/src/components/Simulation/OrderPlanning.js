import React from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';
import { Paper } from '@material-ui/core';

import Form from '../ui_components/Form';
import Text from '../ui_components/Text';
import MultiSelect from '../ui_components/Select';

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
};

const OrderPlanning = ({
  classes,
  language,
  orderPlan,
  changeOrderPlan,
  lastPeriodResults,
}) => {
  const getComponents = () => {
    const elements = [];
    const formProps = {
      obj: orderPlan || undefined,
      setObjState: changeOrderPlan,
    };

    if (orderPlan) {
      let keys = [];
      Object.keys(orderPlan[0]).map((key) => keys.push(key));
      keys = keys.filter((val) =>
        language === 'en' ? val !== 'liefertermin' : val !== 'lieferterminEng'
      );

      keys.map((artKey) =>
        artKey === 'bestellart'
          ? elements.push(
              <div
                key={`order_planning_select_${artKey}_wrapper`}
                style={{ marginRight: '1rem' }}
              >
                <MultiSelect
                  {...formProps}
                  label={<Translate id={`OrderPlanning.${artKey}`} />}
                  prop={artKey}
                />
              </div>
            )
          : artKey === 'menge'
          ? elements.push(
              <div
                key={`order_planning_form_${artKey}_wrapper`}
                style={{ marginRight: '1rem' }}
              >
                <Form
                  {...formProps}
                  key={`capacity_planning_form_${artKey}`}
                  label={<Translate id={`OrderPlanning.${artKey}`} />}
                  prop={artKey}
                  medium
                />
              </div>
            )
          : elements.push(
              <Text
                key={`order_planning_text_${artKey}`}
                obj={orderPlan}
                idProp='matNr'
                label={
                  artKey.slice(0, -1) === 'bedarfPeriode' ||
                  artKey.slice(0, -1) === 'lagerbestandPeriode' ? (
                    <div>
                      <Translate id={`OrderPlanning.${artKey.slice(0, -1)}`} />{' '}
                      {lastPeriodResults
                        ? lastPeriodResults.period + parseInt(artKey.slice(-1))
                        : 0}
                    </div>
                  ) : (
                    <Translate id={`OrderPlanning.${artKey}`} />
                  )
                }
                prop={artKey}
                productIDs
                stickyIDs={artKey === 'matNr'}
              />
            )
      );
    }

    return <div className={classes.root}>{orderPlan ? elements : null}</div>;
  };
  return (
    <Paper classes={{ root: classes.paper }} elevation={3}>
      <div className={classes.wrapper}>
        <form className={classes.form} noValidate autoComplete='off'>
          {getComponents()}
        </form>
      </div>
    </Paper>
  );
};

export default withStyles(styles)(OrderPlanning);
