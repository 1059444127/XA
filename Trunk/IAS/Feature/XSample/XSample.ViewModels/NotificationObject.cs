/// ------------------------------------------------------------------------------
/// <copyright company="United-Imaging">
/// Copyright (c) United-Imaging. All rights reserved.
/// </copyright>
/// <description>
/// </description>
/// ------------------------------------------------------------------------------


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Linq.Expressions;

namespace UIH.XA.XSample.ViewModels
{
    ///<summary>    
    /// 实现接口 "INotifyPropertyChanged"
    ///</summary>    
    public abstract class NotificationObject : INotifyPropertyChanged
    {
        #region NotifyPropertyChanged

        /// <summary>        
        /// 当绑定值改变时触发的事件
        /// </summary>       
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>        
        /// 已类型安全的方式激发事件 e.g [RaisePropertyChanged(() => this.Property)].        
        /// </summary>        
        /// <param name="property">Expression表达式</param>        
        protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            RaisePropertyChanged(GetPropertyNameFromExpression(property));
        }

        /// <summary>        
        /// 激发事件
        /// </summary>       
        /// <param name="propertyName">属性名</param>       
        protected void RaisePropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>        
        /// 执行事件
        /// </summary>       
        /// <param name="e">事件参数</param>        
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            OnPropertyChanged(this, e);
        }

        /// <summary>       
        /// 执行事件时,检查事件实例存在
        /// </summary>        
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var handler = PropertyChanged;
            if (handler == null) return;
            handler(sender, e);
        }


        private string GetPropertyNameFromExpression<T>(Expression<Func<T>> property)
        {
            return ((MemberExpression)property.Body).Member.Name;
        }

        #endregion


        public string Description
        {
            get { return _description; }
            set { _description = value; }
        }
        protected string _description;

    }
}
