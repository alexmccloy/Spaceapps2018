package com.ultra.asuper.globalwarmingisnotcool;

import com.github.mikephil.charting.data.Entry;
import com.github.mikephil.charting.data.LineData;

import java.util.List;

public class CardObject {
    private String mText1;
    private String mText2;
    private LineData mLineData;
    private List<String> mDateLabels;

    CardObject (String text1, String text2, LineData lineData, List<String> dateLabels){
        mText1 = text1;
        mText2 = text2;
        mLineData = lineData;
        mDateLabels = dateLabels;
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

    public List<String> getmDateLabels() {
        return mDateLabels;
    }

    public void setmDateLabels(List<String> mDateLabels) {
        this.mDateLabels = mDateLabels;
    }
}
