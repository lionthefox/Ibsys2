﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.42000
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace="", IsNullable=false)]
public partial class output {
    
    private inputQualitycontrol qualitycontrolField;
    
    private inputItem[] sellwishField;
    
    private inputItem1[] selldirectField;
    
    private inputOrder[] orderlistField;
    
    private inputProduction[] productionlistField;
    
    private inputWorkingtime[] workingtimelistField;
    
    /// <remarks/>
    public inputQualitycontrol qualitycontrol {
        get {
            return this.qualitycontrolField;
        }
        set {
            this.qualitycontrolField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable=false)]
    public inputItem[] sellwish {
        get {
            return this.sellwishField;
        }
        set {
            this.sellwishField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable=false)]
    public inputItem1[] selldirect {
        get {
            return this.selldirectField;
        }
        set {
            this.selldirectField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("order", IsNullable=false)]
    public inputOrder[] orderlist {
        get {
            return this.orderlistField;
        }
        set {
            this.orderlistField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("production", IsNullable=false)]
    public inputProduction[] productionlist {
        get {
            return this.productionlistField;
        }
        set {
            this.productionlistField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("workingtime", IsNullable=false)]
    public inputWorkingtime[] workingtimelist {
        get {
            return this.workingtimelistField;
        }
        set {
            this.workingtimelistField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class inputQualitycontrol {
    
    private string typeField;
    
    private sbyte losequantityField;
    
    private bool losequantityFieldSpecified;
    
    private sbyte delayField;
    
    private bool delayFieldSpecified;
    
    private string valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type {
        get {
            return this.typeField;
        }
        set {
            this.typeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte losequantity {
        get {
            return this.losequantityField;
        }
        set {
            this.losequantityField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool losequantitySpecified {
        get {
            return this.losequantityFieldSpecified;
        }
        set {
            this.losequantityFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte delay {
        get {
            return this.delayField;
        }
        set {
            this.delayField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool delaySpecified {
        get {
            return this.delayFieldSpecified;
        }
        set {
            this.delayFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class inputItem {
    
    private sbyte articleField;
    
    private bool articleFieldSpecified;
    
    private short quantityField;
    
    private bool quantityFieldSpecified;
    
    private string valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte article {
        get {
            return this.articleField;
        }
        set {
            this.articleField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool articleSpecified {
        get {
            return this.articleFieldSpecified;
        }
        set {
            this.articleFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public short quantity {
        get {
            return this.quantityField;
        }
        set {
            this.quantityField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool quantitySpecified {
        get {
            return this.quantityFieldSpecified;
        }
        set {
            this.quantityFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class inputItem1 {
    
    private sbyte articleField;
    
    private bool articleFieldSpecified;
    
    private short quantityField;
    
    private bool quantityFieldSpecified;
    
    private float priceField;
    
    private bool priceFieldSpecified;
    
    private float penaltyField;
    
    private bool penaltyFieldSpecified;
    
    private string valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte article {
        get {
            return this.articleField;
        }
        set {
            this.articleField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool articleSpecified {
        get {
            return this.articleFieldSpecified;
        }
        set {
            this.articleFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public short quantity {
        get {
            return this.quantityField;
        }
        set {
            this.quantityField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool quantitySpecified {
        get {
            return this.quantityFieldSpecified;
        }
        set {
            this.quantityFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public float price {
        get {
            return this.priceField;
        }
        set {
            this.priceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool priceSpecified {
        get {
            return this.priceFieldSpecified;
        }
        set {
            this.priceFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public float penalty {
        get {
            return this.penaltyField;
        }
        set {
            this.penaltyField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool penaltySpecified {
        get {
            return this.penaltyFieldSpecified;
        }
        set {
            this.penaltyFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class inputOrder {
    
    private sbyte articleField;
    
    private bool articleFieldSpecified;
    
    private short quantityField;
    
    private bool quantityFieldSpecified;
    
    private sbyte modusField;
    
    private bool modusFieldSpecified;
    
    private string valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte article {
        get {
            return this.articleField;
        }
        set {
            this.articleField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool articleSpecified {
        get {
            return this.articleFieldSpecified;
        }
        set {
            this.articleFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public short quantity {
        get {
            return this.quantityField;
        }
        set {
            this.quantityField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool quantitySpecified {
        get {
            return this.quantityFieldSpecified;
        }
        set {
            this.quantityFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte modus {
        get {
            return this.modusField;
        }
        set {
            this.modusField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool modusSpecified {
        get {
            return this.modusFieldSpecified;
        }
        set {
            this.modusFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class inputProduction {
    
    private sbyte articleField;
    
    private bool articleFieldSpecified;
    
    private short quantityField;
    
    private bool quantityFieldSpecified;
    
    private string valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte article {
        get {
            return this.articleField;
        }
        set {
            this.articleField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool articleSpecified {
        get {
            return this.articleFieldSpecified;
        }
        set {
            this.articleFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public short quantity {
        get {
            return this.quantityField;
        }
        set {
            this.quantityField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool quantitySpecified {
        get {
            return this.quantityFieldSpecified;
        }
        set {
            this.quantityFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class inputWorkingtime {
    
    private sbyte stationField;
    
    private bool stationFieldSpecified;
    
    private sbyte shiftField;
    
    private bool shiftFieldSpecified;
    
    private short overtimeField;
    
    private bool overtimeFieldSpecified;
    
    private string valueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte station {
        get {
            return this.stationField;
        }
        set {
            this.stationField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool stationSpecified {
        get {
            return this.stationFieldSpecified;
        }
        set {
            this.stationFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public sbyte shift {
        get {
            return this.shiftField;
        }
        set {
            this.shiftField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool shiftSpecified {
        get {
            return this.shiftFieldSpecified;
        }
        set {
            this.shiftFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public short overtime {
        get {
            return this.overtimeField;
        }
        set {
            this.overtimeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public bool overtimeSpecified {
        get {
            return this.overtimeFieldSpecified;
        }
        set {
            this.overtimeFieldSpecified = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}