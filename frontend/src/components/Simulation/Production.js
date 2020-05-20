import React, { useState } from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';

import ContainedTabs from '../ui_components/ContainedTabs';
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
    justifyContent: 'space-around',
    marginTop: '2rem',
  },
};

const Production = ({
  classes,
  simulationInput,
  setSimulationInput,
  lastPeriodResults,
}) => {
  const [index, setIndex] = useState(0);

  const getComponent = () => {
    const formProps = {
      obj: simulationInput,
      setObjState: setSimulationInput,
    };

    switch (index) {
      case 0:
        return (
          <div className={classes.root}>
            <Text
              label={<Translate id='Production.product' />}
              text={[
                <Translate id='Bike.child_bike' />,
                <Translate id='Bike.woman_bike' />,
                <Translate id='Bike.man_bike' />,
              ]}
            />
            <Form
              {...formProps}
              label={lastPeriodResults ? lastPeriodResults.period + 1 : 0}
              values={[
                ['forecast', 'periode1', 'produkt1'],
                ['forecast', 'periode1', 'produkt2'],
                ['forecast', 'periode1', 'produkt3'],
              ]}
            />
            <Form
              {...formProps}
              label={lastPeriodResults ? lastPeriodResults.period + 2 : 0}
              values={[
                ['forecast', 'periode2', 'produkt1'],
                ['forecast', 'periode2', 'produkt2'],
                ['forecast', 'periode2', 'produkt3'],
              ]}
            />
            <Form
              {...formProps}
              label={lastPeriodResults ? lastPeriodResults.period + 3 : 0}
              values={[
                ['forecast', 'periode3', 'produkt1'],
                ['forecast', 'periode3', 'produkt2'],
                ['forecast', 'periode3', 'produkt3'],
              ]}
            />
            <Form
              {...formProps}
              label={lastPeriodResults ? lastPeriodResults.period + 4 : 0}
              values={[
                ['forecast', 'periode4', 'produkt1'],
                ['forecast', 'periode4', 'produkt2'],
                ['forecast', 'periode4', 'produkt3'],
              ]}
            />
          </div>
        );
      case 1:
        return (
          <div className={classes.root}>
            <Text
              label={<Translate id='Production.product' />}
              text={[
                <Translate id='Bike.child_bike' />,
                <Translate id='Bike.woman_bike' />,
                <Translate id='Bike.man_bike' />,
              ]}
            />
            <Form
              {...formProps}
              label={<Translate id='Production.amount' />}
              values={[
                ['vertriebswunsch', 'produkt1'],
                ['vertriebswunsch', 'produkt2'],
                ['vertriebswunsch', 'produkt3'],
              ]}
            />
          </div>
        );
      case 2:
        return (
          <div className={classes.root}>
            <Text
              label={<Translate id='Production.product' />}
              text={[
                <Translate id='Bike.child_bike' />,
                <Translate id='Bike.woman_bike' />,
                <Translate id='Bike.man_bike' />,
              ]}
            />
            <Form
              {...formProps}
              label={<Translate id='Production.amount' />}
              values={[
                ['vertriebswunsch', 'direktverkauf', 'produkt1', 'menge'],
                ['vertriebswunsch', 'direktverkauf', 'produkt2', 'menge'],
                ['vertriebswunsch', 'direktverkauf', 'produkt3', 'menge'],
              ]}
            />
            <Form
              {...formProps}
              decimal
              label={<Translate id='Production.price' />}
              values={[
                ['vertriebswunsch', 'direktverkauf', 'produkt1', 'preis'],
                ['vertriebswunsch', 'direktverkauf', 'produkt2', 'preis'],
                ['vertriebswunsch', 'direktverkauf', 'produkt3', 'preis'],
              ]}
            />
            <Form
              {...formProps}
              decimal
              label={<Translate id='Production.penalty' />}
              values={[
                [
                  'vertriebswunsch',
                  'direktverkauf',
                  'produkt1',
                  'konventionalstrafe',
                ],
                [
                  'vertriebswunsch',
                  'direktverkauf',
                  'produkt2',
                  'konventionalstrafe',
                ],
                [
                  'vertriebswunsch',
                  'direktverkauf',
                  'produkt3',
                  'konventionalstrafe',
                ],
              ]}
            />
            <Text
              obj={lastPeriodResults.warehousestock.article}
              idProp='id'
              idArray={[1, 2, 3]}
              label={<Translate id='Production.stock' />}
              prop='amount'
            />
          </div>
        );
      default:
        return <div>Production</div>;
    }
  };

  return (
    <div className={classes.wrapper}>
      <ContainedTabs
        tabs={[
          { label: <Translate id='Production.forecast' /> },
          { label: <Translate id='Production.sell_wish' /> },
          { label: <Translate id='Production.direct_sales' /> },
        ]}
        value={index}
        onChange={(e, i) => setIndex(i)}
      />
      <form noValidate autoComplete='off'>
        {getComponent()}
      </form>
    </div>
  );
};

export default withStyles(styles)(Production);
