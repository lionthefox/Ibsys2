import React from 'react';

const Text = ({ classes, obj, idProp, label, prop, text, idArray }) => {
  const elements = [];
  if (prop) {
    idArray.map((id) => {
      elements.push(
        <div key={`Text_${label || 'label'}_${id}`} className={classes.value}>
          {obj
            ? obj.map((art) => {
                if (art[idProp] === id) return art[prop] || 0;
              })
            : 0}
        </div>
      );
    });
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

export default Text;
