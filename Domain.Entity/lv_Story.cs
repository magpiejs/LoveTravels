﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity
{
    /// <summary>
    /// 旅途故事
    /// </summary>
    public class lv_Story
    {
        /// <summary>
        /// 自动增长
        /// </summary>
        [Key]
        public int ID { get; set; }

        /// <summary>
        /// 发布人
        /// </summary>
         [Display(Name = "发布人")]
        public int UserId { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [MaxLength(50)]
        [Display(Name = "作者")]
        public string Author { get; set; }

        /// <summary>
        /// 故事标题
        /// </summary>
        [MaxLength(500)]
        [Display(Name = "故事标题")]
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        [Display(Name = "副标题")]
        [MaxLength(1000)]
        public string SubTitle { get; set; }

        /// <summary>
        /// 标签
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "标签")]
        public string Tag { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        [Display(Name = "访问量")]
        public int Hits { get; set; }

        /// <summary>
        /// 是否优质精华
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "是否优质精华")]
        public string IsTop { get; set; }

        /// <summary>
        /// 地区
        /// </summary>
        [MaxLength(200)]
        [Display(Name = "地区")]
        public string Country { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        [Display(Name = "封面图")]
        [MaxLength(500)]
        public string CoverImg { get; set; }

        /// <summary>
        /// 故事内容
        /// </summary>
        [Display(Name = "故事内容")]
        public string Centents { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [Display(Name = "更新时间")]
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 添加时间
        /// </summary>
        [Display(Name = "添加时间")]
        public DateTime AddTime { get; set; }

        /// <summary>
        /// 关联到用户表
        /// </summary>
        [ForeignKey("UserId")]
        public virtual tb_User tb_User { get; set; }
    }
}
