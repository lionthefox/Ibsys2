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

const QuantityPlanning = ({
  classes,
  simulationData,
  setSimulationData,
  activeLanguage,
}) => {
  const [index, setIndex] = useState(0);

  console.log(simulationData);
  const products = ['p1', 'p2', 'p3'];

  const getComponent = (productIndex) => {
    const elements = [];
    const formProps = {
      obj:
        (simulationData && simulationData[products[productIndex]]) || undefined,
      setObj: setSimulationData,
    };

    if (simulationData) {
      let keys = [];
      Object.keys(simulationData[products[productIndex]][0]).map((key) =>
        keys.push(key)
      );
      keys = keys.filter((val) => {
        if (val === 'vertrieb' || val === 'auftragUebernahme') return false;
        if (activeLanguage === 'en') {
          return val !== 'name';
        } else {
          return val !== 'nameEng';
        }
      });

      keys.map((artKey) =>
        artKey === 'sicherheitsbestand'
          ? elements.push(
              <Form
                key={`quantity_planning_form_${productIndex}_${artKey}`}
                {...formProps}
                label={<Translate id={`QuantityPlanning.${artKey}`} />}
                prop='sicherheitsbestand'
              />
            )
          : elements.push(
              <Text
                key={`quantity_planning_text_${productIndex}_${artKey}`}
                obj={simulationData[products[productIndex]]}
                idProp='articleId'
                label={<Translate id={`QuantityPlanning.${artKey}`} />}
                prop={artKey}
              />
            )
      );
    }

    return (
      <div className={classes.root}>{simulationData ? elements : null}</div>
    );
  };

  return (
    <div className={classes.wrapper}>
      <ContainedTabs
        tabs={[
          { label: <Translate id='Bike.child_bike' /> },
          { label: <Translate id='Bike.woman_bike' /> },
          { label: <Translate id='Bike.man_bike' /> },
        ]}
        value={index}
        onChange={(e, i) => setIndex(i)}
      />
      <form noValidate autoComplete='off'>
        {getComponent(index)}
      </form>
    </div>
  );
};

export default withStyles(styles)(QuantityPlanning);
