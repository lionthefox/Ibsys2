export const getFloatValue = (val, decimal) => {
  let newVal = val;
  if (val && !decimal) {
    newVal = val.replace('.', '');
  }
  return val ? parseFloat(newVal) : 0;
};
