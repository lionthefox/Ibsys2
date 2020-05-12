import React, { useState } from 'react';
import Production from '../components/Simulation/Production';

import ContainedTabs from '../components/ui_components/ContainedTabs';

export default {
  title: 'Production',
  component: Production,
};

export const ProductionExample = () => {
  const [index, setIndex] = useState(0);
  return (
    <ContainedTabs
      style={{ alignSelf: 'flex-end' }}
      tabs={[
        { label: 'Example' },
        { label: 'Comparison' },
        { label: 'Reviews' },
        { label: 'Return Policy' },
      ]}
      value={index}
      onChange={(e, i) => setIndex(i)}
    />
  );
};
