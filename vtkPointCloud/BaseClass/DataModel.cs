﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
namespace vtkPointCloud
{
    public class mergeGroup {
        public int MergeId{get;set;}
        public int OriginId { get; set; }
    }
    [Serializable] 
    public class ClusObj : ICloneable
    {
        public List<Point3D> li { get; set; }
        public int clusId{get;set;}
        public string clusName { get; set;}
        public bool visible { get; set; }
        public int ptsCount;
        //public ClusObj(List<Point3D> l, int id)
        //{
        //    this.clusId = id;
        //    this.li = null;
        //    this.li = l;
        //    this.ptsCount = l.Count;
        //}
        //public ClusObj(List<Point3D> l) {
        //    this.li = null;
        //    this.li = l;
        //    this.ptsCount = l.Count;
        //}
        public ClusObj() { this.li = new List<Point3D>(); this.visible = true; }
        public ClusObj(String s)
        {
            this.clusName = s;
            this.li = new List<Point3D>();
            this.visible = true;
        }
        public Object Clone() //深度拷贝  
        {
            using (MemoryStream ms = new MemoryStream(1000))
            {
                object CloneObject;
                BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                bf.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                // 反序列化至另一个对象(即创建了一个原对象的深表副本)   
                CloneObject = bf.Deserialize(ms);
                // 关闭流   
                ms.Close();
                return CloneObject;
            }
            //另一种序列化与反序列化进行深拷贝
            //using (Stream objectStream = new MemoryStream())
            //{
            //    //利用 System.Runtime.Serialization序列化与反序列化完成引用对象的复制  
            //    IFormatter formatter = new BinaryFormatter();
            //    formatter.Serialize(objectStream, RealObject);
            //    objectStream.Seek(0, SeekOrigin.Begin);
            //    return (T)formatter.Deserialize(objectStream);
            //}   
        }
    }
     [Serializable]
    public class Point2D : ICloneable
    {
        public double x { get; set; }
        public double y { get; set; }
        public int clusID { get; set; }
        public bool isFilter{ get;set; }
        public double radius { get; set; }
        public Point2D(double xx, double yy)
        {
            this.x = xx;
            this.y = yy;
        }
        public Point2D(double xx, double yy, int cluID) {
            this.x = xx;
            this.y = yy;
            this.clusID = cluID;
        }
        public Object Clone() //深度拷贝  
        {
            using (MemoryStream ms = new MemoryStream(1000))
            {
                object CloneObject;
                BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                bf.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                // 反序列化至另一个对象(即创建了一个原对象的深表副本)   
                CloneObject = bf.Deserialize(ms);
                // 关闭流   
                ms.Close();
                return CloneObject;
            }
        }
    };

    //3维点所有属性 包括电机xyz 球面坐标xyz 聚类ID 是否已被分类 是否已被匹配 是否被遍历到 是否是关键点 路径Id
    [Serializable] 
    public class Point3D :ICloneable
    {
        public Point3D() { }
        public Point3D(double xx, double yy, double zz)
        {
            this.X = xx;
            this.Y = yy;
            this.Z = zz;
        }
        public Point3D(double xx, double yy, double zz, int clusterId ,bool isShown)
        {
            this.X = xx;
            this.Y = yy;
            this.Z = zz;
            this.clusterId = clusterId;
            this.ifShown = isShown;
            //this.isTraversal = false;
        }
        public int IDBeforeMerge { get; set; }
        public double motor_x { get; set; }//点机x
        public double motor_y { get; set; }//点机y
        public double Distance { get; set; }//距离
        public double X { get; set; }//三维x
        public double Y { get; set; }//三维y
        public double Z { get; set; }//三维z
        public int clusterId { get; set; }//聚类id
        public int pathId { get; set; }//路径id
        //public bool isTraversal { get; set; }//是否已遍历
        public Boolean ifShown { get; set; }//是否显示
        //public Boolean isFilter { get; set; }//是否过滤
        public int ptsCount { get; set; }
        public Boolean isClassed { get; set; }//是否已被分类
        public Boolean isKeyPoint { get; set; }//是否是关键点
        public Boolean isMatched { get; set; }//是否已被匹配
        public int matchNum { get; set; }//匹配号
        public string pointName { get; set; }//真值点名
        public double tmp_X { get; set; }//转换暂时坐标x
        public double tmp_Y { get; set; }//转换暂时坐标y
        public double tmp_Z { get; set; }//转换暂时坐标z
        public double matched_X { get; set; }//匹配坐标x
        public double matched_Y { get; set; }//匹配坐标y
        public double matched_Z { get; set; }//匹配坐标z
        public Boolean isFilterByDistance { get; set; }//是否被距离阈值过滤
        public Object Clone() //深度拷贝  
        {
            using (MemoryStream ms = new MemoryStream(1000))
            {
                object CloneObject;
                BinaryFormatter bf = new BinaryFormatter(null, new StreamingContext(StreamingContextStates.Clone));
                bf.Serialize(ms, this);
                ms.Seek(0, SeekOrigin.Begin);
                // 反序列化至另一个对象(即创建了一个原对象的深表副本)   
                CloneObject = bf.Deserialize(ms);
                // 关闭流   
                ms.Close();
                return CloneObject;
            }
        }
    };
    public static class CloneForList{
        public static IList<Point3D> Clone<Point3D>(this IList<Point3D> source)
where Point3D : ICloneable
        {
            IList<Point3D> newList = new List<Point3D>(source.Count);
            foreach (var item in source)
            {
                newList.Add((Point3D)((ICloneable)item.Clone()));
            }
            return newList;
        }
}
    public class Line2D
    {
        public Point2D startPoint;
        public Point2D endPoint;
    };

    public class Line3D
    {
        public Point3D startPoint;
        public Point3D endPoint;
    };

    public class Pixel
    {
        public int x;
        public int y;
        public int value;
    }
    public class Rectangle2D {
         public Rectangle2D() { }
         public Rectangle2D(double xx, double yy, double width, double height)
        {
            this.xx = xx;
            this.yy = yy;
            this.width = width;
            this.height = height;
        }
         public double xx { get; set; }
         public double yy{get;set;}
         public double width { get; set; }
         public double height { get; set; }
         public double Top { get;set; }
         public double Right { get;set; }
         public double Left { get; set;}
         public int Bottom { get; set; }
    }
}
