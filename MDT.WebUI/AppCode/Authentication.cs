using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;


/// <summary>
/// Authentication ��ժҪ˵��
/// </summary>
public class Authentication : System.Web.Services.Protocols.SoapHeader
{


    private string _userName;
    private string _userPasswd;
    public Authentication()
    {

    }

    /// <summary>
    /// �ǳ�
    /// </summary>
    public string UserName
    {
        set
        {
            this._userName = value;
        }
        get
        {
            return this._userName;
        }
    }

    public string UserPasswd
    {
        set
        {
            this._userPasswd = value;
        }
        get
        {
            return this._userPasswd;
        }
    }
    //��Ҫдһ����⹤��ʦ�û����������Ƿ���ڵķ���

    public bool CheckAuthentication()
    {
        //User user = new User();

        //return user.CheckExistUserByUserNameAndPassword(this._userName, this._userPasswd);
        return true;
    }

}