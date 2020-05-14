export const getNestedObjectProperty = (theObject, keyArray) => {
  let result = { ...theObject };
  keyArray.map((val) => {
    result = result[val];
  });
  return result;
};

export const setNestedObjectProperty = (theObject, keyArray, value) => {
  let result = { ...theObject };
  let index = 0;
  const setValue = (obj) => {
    Object.keys(obj).map((key) => {
      if (keyArray[index] && keyArray[index].toString() === key) {
        if (index === keyArray.length - 1) {
          obj[key] = value;
        } else {
          index++;
          setValue(obj[key]);
        }
      }
    });
  };
  setValue(result);
  return result;
};
