import React, { useState } from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';
import { Paper, ListItemIcon } from '@material-ui/core';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import { DragDropContext, Droppable, Draggable } from 'react-beautiful-dnd';

import VerticalSplitRoundedIcon from '@material-ui/icons/VerticalSplitRounded';
import ContainedTabs from '../ui_components/ContainedTabs';

const styles = {
  wrapper: {
    width: '100%',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
  },
  root: {
    display: 'flex',
    width: '80vw',
    flexDirection: 'column',
    justifyContent: 'space-around',
  },
  paper: {
    marginTop: '1rem',
    height: 'calc(100vh - 460px)',
    overflow: 'auto',
    padding: '0 10px',
  },
  elementContainer: {
    display: 'flex',
    width: '100%',
    height: '4rem',
    marginBottom: '8px',
    background: '#fff',
    fontSize: '20px',
    borderRadius: '10px',
    boxShadow: '1px 2px 8px 0px rgba(194,194,194,0.5)',
    '&:hover': {
      background: 'rgba(19, 83, 67, 0.25)',
    },
    '&:active': {
      background: 'rgba(19, 83, 67, 0.8)',
    },
  },
  articleId: {
    width: '11.5%',
    paddingLeft: '10px',
  },
  name: {
    width: '66%',
  },
  productionAmount: {
    width: '15%',
  },
  splitButton: {
    color: '#fff',
    padding: '5px',
    borderRadius: '10px',
    background: '#135444',
  },
  headerContainer: {
    display: 'flex',
    width: '100%',
    height: '4rem',
    marginBottom: '8px',
    background: '#fff',
    fontSize: '20px',
    color: '#135444',
    zIndex: 1000,
    position: 'sticky',
    top: 0,
    padding: '1rem 10px 0',
    margin: '0 -10px',
  },
  articleIdHeader: {
    width: '12.8%',
    height: '100%',
    display: 'flex',
    alignItems: 'center',
    paddingLeft: '1rem',
  },
  nameHeader: {
    width: '65%',
    height: '100%',
    display: 'flex',
    alignItems: 'center',
  },
  productionAmountHeader: {
    width: '10%',
    height: '100%',
    display: 'flex',
    alignItems: 'center',
    paddingRight: '62px',
  },
  splittingHeader: {
    height: '100%',
    display: 'flex',
    alignItems: 'center',
  },
};
//TODO: ArticleIDs auflösen
//TODO: Splitting mit Runden
const SequencePlanning = ({
  classes,
  language,
  simulationData,
  setSimulationData,
}) => {
  const [index, setIndex] = useState(0);
  const products = ['p1', 'p2', 'p3'];
  const onDragEnd = (result) => {
    const { destination, source } = result;
    if (!destination) {
      return;
    }
    if (
      destination.droppableId === source.droppableId &&
      destination.index === source.index
    ) {
      return;
    }
    const newSimulationData = { ...simulationData };
    const draggedElement = newSimulationData[products[index]][source.index];
    newSimulationData[products[index]].splice(source.index, 1);
    newSimulationData[products[index]].splice(
      destination.index,
      0,
      draggedElement
    );
    setSimulationData(newSimulationData);
  };

  const getComponents = () => {
    const elements = [];

    if (simulationData) {
      simulationData[products[index]].map((val, keyIndex) => {
        elements.push(
          <Draggable
            draggableId={`sequence_planning_${products[index]}_${keyIndex}`}
            index={keyIndex}
            key={`sequence_planning_${products[index]}_${keyIndex}`}
          >
            {(provided) => (
              <ListItem
                {...provided.draggableProps}
                {...provided.dragHandleProps}
                innerRef={provided.innerRef}
                classes={{ root: classes.elementContainer }}
              >
                <ListItemText
                  classes={{ root: classes.articleId }}
                  primary={val.articleId}
                />
                <ListItemText
                  classes={{ root: classes.name }}
                  primary={language === 'en' ? val.nameEng : val.name}
                />
                <ListItemText
                  classes={{ root: classes.productionAmount }}
                  primary={val.produktion}
                />
                <ListItemIcon>
                  <VerticalSplitRoundedIcon
                    classes={{ root: classes.splitButton }}
                  />
                </ListItemIcon>
              </ListItem>
            )}
          </Draggable>
        );
      });
    }
    return (
      <div className={classes.root}>
        <div className={classes.headerContainer}>
          <div className={classes.articleIdHeader}>
            <Translate id='QuantityPlanning.articleId' />
          </div>
          <div className={classes.nameHeader}>
            <Translate id='QuantityPlanning.name' />
          </div>
          <div className={classes.productionAmountHeader}>
            <Translate id='QuantityPlanning.produktion' />
          </div>
          <div className={classes.splittingHeader}>
            <Translate id='SequencePlanning.splitting' />
          </div>
        </div>
        <Droppable droppableId='sequene_planning_droppable'>
          {(provided) => (
            <List
              component='nav'
              aria-label='sequence_planning_list'
              innerRef={provided.innerRef}
              {...provided.droppableProps}
            >
              {simulationData ? elements : null}
              {provided.placeholder}
            </List>
          )}
        </Droppable>
      </div>
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
        <DragDropContext onDragEnd={onDragEnd}>
          {getComponents()}
        </DragDropContext>
      </Paper>
    </div>
  );
};

export default withStyles(styles)(SequencePlanning);
