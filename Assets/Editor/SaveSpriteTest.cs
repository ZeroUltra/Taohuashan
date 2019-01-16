using UnityEngine;
using UnityEditor;


/// <summary>
/// 如何把一张图集中的单个图片导出来成为贴图(NGUI使用)
/// </summary>
public class SaveSpriteTest {
   
   //在菜单选项栏加一个菜单
    [MenuItem("Tools/切割图片")]
   public static void Check()
    {
        Debug.Log("111");
        //定义一个字符串(是用来检查是否符合在规定的路径)
        string resourcsePath ="Assets/Resources/";
        //对在编辑状态下,所选的对象(就是鼠标选中的对象)(这里是选择的一张贴图)
        foreach (Object obj in Selection.objects)
        {
            Debug.Log("222");

            //得到选中物体的路径
            string selectionPath = AssetDatabase.GetAssetPath(obj);
            //Debug.Log(selectionPath);
            //如果选中物体的路劲是规定的路径(以防选错)(用字符串来判断)
            if (selectionPath.StartsWith(resourcsePath))
            {

                //得到当前路径下文件的扩展名(eg:.png)
                string selectionExt = System.IO.Path.GetExtension(selectionPath);
               // Debug.Log(selectionExt);
                if (selectionExt.Length == 0)
                {
                    continue;
                }
                //以下三行代码是规定resources的加载路径
                char[] tt = resourcsePath.ToCharArray();
                //--选择的路径减去扩展名,然后去掉前面的文件路径
                string temploadPath = selectionPath.Remove(selectionPath.Length - selectionExt.Length);
                string loadPath=temploadPath.TrimStart(tt);
           
                //加载
                Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);
                Debug.Log(sprites.Length);
                if (sprites.Length>0)
                {
                    Debug.Log("开始切割");
                    //输出路径
                    string outPath = Application.dataPath + "/outsprites" + "";
                    //创建一个文件
                    System.IO.Directory.CreateDirectory(outPath);
                    //然后将每张精灵图片转换为贴图
                    foreach (Sprite spitem in sprites)
                    {
                        Texture2D tex = new Texture2D((int)spitem.rect.width, (int)spitem.rect.height, spitem.texture.format, false);
                        tex.SetPixels(spitem.texture.GetPixels((int)spitem.rect.xMin, (int)spitem.rect.yMin, (int)spitem.rect.width, (int)spitem.rect.height));
                        tex.Apply();
                        //将每一张图片写入
                        System.IO.File.WriteAllBytes(outPath + "/" + spitem.name + selectionExt, tex.EncodeToPNG());
                    }
                    Debug.Log("保存到了" + outPath);
                }
            }
            else
            {
                Debug.LogError("路径有错");
            }

           
           // string name=selection
        }
    }
}
