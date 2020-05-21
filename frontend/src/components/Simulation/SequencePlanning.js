import React, { useState } from 'react';
import { withStyles } from '@material-ui/core/styles';
import { Translate } from 'react-localize-redux';
import { Paper } from '@material-ui/core';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import ListItemText from '@material-ui/core/ListItemText';
import { DragDropContext, Droppable, Draggable } from 'react-beautiful-dnd';

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
    padding: '10px',
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
  },
};

const SequencePlanning = ({ classes, simulationData, setSimulationData }) => {
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
                <ListItemText primary={val.articleId} />
              </ListItem>
            )}
          </Draggable>
        );
      });
    }
    return (
      <div className={classes.root}>
        <div className={classes.elementContainer}></div>
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
