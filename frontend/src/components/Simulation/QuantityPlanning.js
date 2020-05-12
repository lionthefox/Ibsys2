import React from 'react';

const QuantityPlanning = ({ simulationData }) => (
  <div>
    {console.log(simulationData)}
    {simulationData ? simulationData['p1'][0]['sicherheitsbestand'] : null}
  </div>
);

export default QuantityPlanning;
