import React from 'react';
import { withStyles } from '@material-ui/core/styles';

const styles = {
  textContainer: {
    display: 'flex',
    flexDirection: 'column',
    marginRight: '2rem',
    marginLeft: '2rem',
  },
  headerLabel: {
    marginBottom: '1.5rem',
    textAlign: 'center',
    fontSize: '18px',
  },
  value: {
    display: 'flex',
    height: '25%',
    alignItems: 'center',
    justifyContent: 'center',
    marginBottom: '3px',
  },
};

const Text = ({ classes, obj, idProp, label, prop, text, idArray }) => {
  const elements = [];
  const getArticleProp = (article) => {
    if (article[prop]) {
      if (prop === idProp) {
        if (
          article[idProp] === 1 ||
          article[idProp] === 2 ||
          article[idProp] === 3
        )
          return `P${article[prop]}`;
        else return `E${article[prop]}`;
      } else return article[prop];
    } else return 0;
  };

  if (prop) {
    if (idArray) {
      idArray.map((id) =>
        elements.push(
          <div key={`Text_${label || 'label'}_${id}`} className={classes.value}>
            {obj
              ? obj.map((art) => {
                  if (art[idProp] === id) {
                    return getArticleProp(art);
                  }
                })
              : 0}
          </div>
        )
      );
    } else {
      if (obj) {
        obj.map((art) =>
          elements.push(
            <div
              key={`Text_${label || 'label'}_${art[idProp]}`}
              className={classes.value}
            >
              {getArticleProp(art)}
            </div>
          )
        );
      }
    }
  } else if (text) {
    text.map((val, index) =>
      elements.push(
        <div
          key={`Production_text_${label || 'label'}_${index}`}
          className={classes.value}
        >
          {val}
        </div>
      )
    );
  }
  return (
    <div className={classes.textContainer}>
      {label ? <div className={classes.headerLabel}>{label}</div> : null}
      <>{elements}</>
    </div>
  );
};

export default withStyles(styles)(Text);
