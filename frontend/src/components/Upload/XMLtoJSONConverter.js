/* const xmlToJson = (xml) => {
  let obj = {};

  if (xml.nodeType === 1) {
    if (xml.attributes.length > 0) {
      for (let j = 0; j < xml.attributes.length; j += 1) {
        const attribute = xml.attributes.item(j);
        Number.isNaN(+attribute.nodeValue)
          ? (obj[attribute.nodeName] = attribute.nodeValue)
          : (obj[attribute.nodeName] = +attribute.nodeValue);
      }
    }
  }

  if (xml.hasChildNodes()) {
    for (let i = 0; i < xml.childNodes.length; i += 1) {
      const item = xml.childNodes.item(i);
      const nodeName = item.nodeName;
      if (nodeName === 'totalstockvalue') {
        obj[nodeName] = +item.childNodes[0].wholeText;
      } else if (item.nodeName !== '#text') {
        if (typeof obj[nodeName] === 'undefined') {
          obj[nodeName] = xmlToJson(item);
        } else {
          if (typeof obj[nodeName].push === 'undefined') {
            const old = obj[nodeName];
            obj[nodeName] = [];
            obj[nodeName].push(old);
          }
          obj[nodeName].push(xmlToJson(item));
        }
      }
    }
  }

  return obj;
};

export default xmlToJson; */
