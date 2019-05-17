
public string dateFormat = "MM-dd-yyyy/hh:mm:ss";
public DateTime dateNow;
IEnumerator GetTime()
{
    PopupManager.ShowLoading();
    string _txtTime;
    var _url = "http://dev-zoom.com/date-time.php";
    WWW www = new WWW(_url);
    yield return www;
    PopupManager.HideLoading();
    _txtTime = www.text;
    Debug.Log(_txtTime);
    try
    {
        dateNow = DateTime.ParseExact(_txtTime, dateFormat, CultureInfo.InvariantCulture);
    }
    catch (Exception)
    {
        string[] _words = _txtTime.Split('/');
        string[] _day = _words[0].Split('-');
        string[] _hour = _words[1].Split(':');
        int MM = int.Parse(_day[0]);
        int dd = int.Parse(_day[1]);
        int yyy = int.Parse(_day[2]);
        int hh = int.Parse(_hour[0]);
        int mm = int.Parse(_hour[1]);
        int ss = int.Parse(_hour[2]);
        dateNow = new DateTime(yyy, MM, dd, hh, mm, ss);
    }
    
    
    }
