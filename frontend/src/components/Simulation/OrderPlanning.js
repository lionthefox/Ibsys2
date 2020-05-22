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
  },
  paper: {
    marginTop: '1rem',
    height: 'calc(100vh - 390px)',
    overflow: 'auto',
  },
};
//TODO: Multiselect Bestellart
const OrderPlanning = ({ classes, language, orderPlan, changeOrderPlan }) => {
  console.log(orderPlan);
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
        artKey === 'bestellart' || artKey === 'menge'
          ? elements.push(
              <div
                key={`capacity_planning_form_${artKey}_wrapper`}
                style={{ marginRight: '1rem' }}
              >
                <Form
                  {...formProps}
                  maxValue={artKey === 'ubermin' ? 240 : 3}
                  key={`capacity_planning_form_${artKey}`}
                  label={<Translate id={`OrderPlanning.${artKey}`} />}
                  prop={artKey}
                  medium
                />
              </div>
            )
          : elements.push(
              <Text
                key={`capacity_planning_text_${artKey}`}
                obj={orderPlan}
                idProp='matNr'
                label={<Translate id={`OrderPlanning.${artKey}`} />}
                prop={artKey}
                productIDs
              />
            )
      );
    }

    return <div className={classes.root}>{orderPlan ? elements : null}</div>;
  };
  return (
    <Paper classes={{ root: classes.paper }} elevation={3}>
      <div className={classes.wrapper}>
        <form noValidate autoComplete='off'>
          {getComponents()}
        </form>
      </div>
    </Paper>
  );
};

export default withStyles(styles)(OrderPlanning);
