package com.ultra.asuper.globalwarmingisnotcool;

import com.github.mikephil.charting.data.Entry;
import com.github.mikephil.charting.data.LineData;

import java.util.List;

public class CardObject {
    private String mText1;
    private String mText2;
    private LineData mLineData;

    CardObject (String text1, String text2, LineData lineData){
        mText1 = text1;
        mText2 = text2;
        mLineData = lineData;
    }

    public String getmText1() {
        return mText1;
    }

    public void setmText1(String mText1) {
        this.mText1 = mText1;
    }

    public String getmText2() {
        return mText2;
    }

    public void setmText2(String mText2) {
        this.mText2 = mText2;
    }

    public LineData getmLineData() {
        return mLineData;
    }

    public void setmLineData(LineData mLineData) {
        this.mLineData = mLineData;
    }
}
