//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Option: missing-value detection (*Specified/ShouldSerialize*/Reset*) enabled
    
// Generated from: c_table_scene.proto
// Note: requires additional types generated from: common_scene.proto
namespace Table
{
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SCENE")]
  public partial class SCENE : global::ProtoBuf.IExtensible
  {
    public SCENE() {}
    

    private uint? _id;
    [global::ProtoBuf.ProtoMember(1, IsRequired = false, Name=@"id", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public uint id
    {
      get { return _id?? default(uint); }
      set { _id = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool idSpecified
    {
      get { return _id != null; }
      set { if (value == (_id== null)) _id = value ? id : (uint?)null; }
    }
    private bool ShouldSerializeid() { return idSpecified; }
    private void Resetid() { idSpecified = false; }
    

    private Common.SceneType? _type;
    [global::ProtoBuf.ProtoMember(2, IsRequired = false, Name=@"type", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public Common.SceneType type
    {
      get { return _type?? Common.SceneType.SCENE_TYPE_INVALID; }
      set { _type = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool typeSpecified
    {
      get { return _type != null; }
      set { if (value == (_type== null)) _type = value ? type : (Common.SceneType?)null; }
    }
    private bool ShouldSerializetype() { return typeSpecified; }
    private void Resettype() { typeSpecified = false; }
    

    private string _name;
    [global::ProtoBuf.ProtoMember(3, IsRequired = false, Name=@"name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string name
    {
      get { return _name?? ""; }
      set { _name = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool nameSpecified
    {
      get { return _name != null; }
      set { if (value == (_name== null)) _name = value ? name : (string)null; }
    }
    private bool ShouldSerializename() { return nameSpecified; }
    private void Resetname() { nameSpecified = false; }
    

    private string _desc;
    [global::ProtoBuf.ProtoMember(4, IsRequired = false, Name=@"desc", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string desc
    {
      get { return _desc?? ""; }
      set { _desc = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool descSpecified
    {
      get { return _desc != null; }
      set { if (value == (_desc== null)) _desc = value ? desc : (string)null; }
    }
    private bool ShouldSerializedesc() { return descSpecified; }
    private void Resetdesc() { descSpecified = false; }
    

    private string _scene_name;
    [global::ProtoBuf.ProtoMember(5, IsRequired = false, Name=@"scene_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string scene_name
    {
      get { return _scene_name?? ""; }
      set { _scene_name = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool scene_nameSpecified
    {
      get { return _scene_name != null; }
      set { if (value == (_scene_name== null)) _scene_name = value ? scene_name : (string)null; }
    }
    private bool ShouldSerializescene_name() { return scene_nameSpecified; }
    private void Resetscene_name() { scene_nameSpecified = false; }
    

    private string _level_name;
    [global::ProtoBuf.ProtoMember(6, IsRequired = false, Name=@"level_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string level_name
    {
      get { return _level_name?? ""; }
      set { _level_name = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool level_nameSpecified
    {
      get { return _level_name != null; }
      set { if (value == (_level_name== null)) _level_name = value ? level_name : (string)null; }
    }
    private bool ShouldSerializelevel_name() { return level_nameSpecified; }
    private void Resetlevel_name() { level_nameSpecified = false; }
    

    private int? _blood;
    [global::ProtoBuf.ProtoMember(7, IsRequired = false, Name=@"blood", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int blood
    {
      get { return _blood?? default(int); }
      set { _blood = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool bloodSpecified
    {
      get { return _blood != null; }
      set { if (value == (_blood== null)) _blood = value ? blood : (int?)null; }
    }
    private bool ShouldSerializeblood() { return bloodSpecified; }
    private void Resetblood() { bloodSpecified = false; }
    

    private int? _jump_count;
    [global::ProtoBuf.ProtoMember(8, IsRequired = false, Name=@"jump_count", DataFormat = global::ProtoBuf.DataFormat.TwosComplement)]
    public int jump_count
    {
      get { return _jump_count?? default(int); }
      set { _jump_count = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool jump_countSpecified
    {
      get { return _jump_count != null; }
      set { if (value == (_jump_count== null)) _jump_count = value ? jump_count : (int?)null; }
    }
    private bool ShouldSerializejump_count() { return jump_countSpecified; }
    private void Resetjump_count() { jump_countSpecified = false; }
    

    private string _hero_name;
    [global::ProtoBuf.ProtoMember(9, IsRequired = false, Name=@"hero_name", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public string hero_name
    {
      get { return _hero_name?? ""; }
      set { _hero_name = value; }
    }
    //Here has been replaced by XXMMLLDeleter
    [global::System.ComponentModel.Browsable(false)]
    public bool hero_nameSpecified
    {
      get { return _hero_name != null; }
      set { if (value == (_hero_name== null)) _hero_name = value ? hero_name : (string)null; }
    }
    private bool ShouldSerializehero_name() { return hero_nameSpecified; }
    private void Resethero_name() { hero_nameSpecified = false; }
    
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
  [global::System.Serializable, global::ProtoBuf.ProtoContract(Name=@"SCENE_ARRAY")]
  public partial class SCENE_ARRAY : global::ProtoBuf.IExtensible
  {
    public SCENE_ARRAY() {}
    
    private readonly global::System.Collections.Generic.List<Table.SCENE> _rows = new global::System.Collections.Generic.List<Table.SCENE>();
    [global::ProtoBuf.ProtoMember(1, Name=@"rows", DataFormat = global::ProtoBuf.DataFormat.Default)]
    public global::System.Collections.Generic.List<Table.SCENE> rows
    {
      get { return _rows; }
    }
  
    private global::ProtoBuf.IExtension extensionObject;
    global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
      { return global::ProtoBuf.Extensible.GetExtensionObject(ref extensionObject, createIfMissing); }
  }
  
}