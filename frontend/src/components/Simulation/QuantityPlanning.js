import React, { useState } from 'react';
import { Paper } from '@material-ui/core';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';
import useMediaQuery from '@material-ui/core/useMediaQuery';

import ContainedTabs from '../ui_components/ContainedTabs';
import Form from '../ui_components/Form';
import Text from '../ui_components/Text';
import clsx from 'clsx';

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
  },
  paper: {
    marginTop: '1rem',
    height: 'calc(100vh - 440px)',
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

const QuantityPlanning = ({
  classes,
  simulationData,
  changeSimulationData,
  activeLanguage,
}) => {
  const [index, setIndex] = useState(0);
  const products = ['p1', 'p2', 'p3'];

  const centered =
    useMediaQuery('(min-width:1500px)') && activeLanguage == 'en';

  const getComponents = () => {
    const elements = [];
    const formProps = {
      obj: (simulationData && simulationData[products[index]]) || undefined,
      setObjState: changeSimulationData,
      product: products[index],
    };

    if (simulationData) {
      let keys = [];
      Object.keys(simulationData[products[index]][0]).map((key) =>
        keys.push(key)
      );
      keys = keys.filter((val) => {
        if (val === 'auftragUebernahme') return false;
        if (activeLanguage === 'en') {
          return val !== 'name';
        } else {
          return val !== 'nameEng';
        }
      });

      keys.map((artKey) => {
        const elementProps = {
          label: <Translate id={`QuantityPlanning.${artKey}`} />,
          prop: artKey,
        };
        artKey === 'sicherheitsbestand'
          ? elements.push(
              <Form
                {...formProps}
                {...elementProps}
                key={`quantity_planning_form_${index}_${artKey}`}
                medium
              />
            )
          : elements.push(
              <Text
                {...elementProps}
                key={`quantity_planning_text_${index}_${artKey}`}
                obj={simulationData[products[index]]}
                idProp='articleId'
                productIDs
              />
            );
      });
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
      <Paper classes={{ root: classes.paper }} elevation={3}>
        <form
          className={clsx(classes.form, { [classes.centered]: centered })}
          noValidate
          autoComplete='off'
        >
          {getComponents()}
        </form>
      </Paper>
    </div>
  );
};

export default withStyles(styles)(QuantityPlanning);
