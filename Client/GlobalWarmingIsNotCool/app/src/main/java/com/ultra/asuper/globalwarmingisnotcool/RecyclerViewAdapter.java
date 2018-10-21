package com.ultra.asuper.globalwarmingisnotcool;

import android.graphics.Bitmap;
import android.graphics.BitmapFactory;
import android.os.AsyncTask;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewManager;
import android.widget.ImageView;
import android.widget.TextView;

import com.github.mikephil.charting.charts.LineChart;
import com.github.mikephil.charting.components.AxisBase;
import com.github.mikephil.charting.components.XAxis;
import com.github.mikephil.charting.formatter.IAxisValueFormatter;
import com.squareup.picasso.Picasso;

import java.io.IOException;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.ArrayList;

public class RecyclerViewAdapter extends RecyclerView
        .Adapter<RecyclerViewAdapter
        .DataObjectHolder> {
    private static String LOG_TAG = "RecyclerViewAdapter";
    private ArrayList<CardObject> mDataset;
    private static MyClickListener myClickListener;

    public static class DataObjectHolder extends RecyclerView.ViewHolder
            implements View
            .OnClickListener {
        TextView label;
        TextView dateTime;
        LineChart chart;
        ImageView image;


        public DataObjectHolder(View itemView) {
            super(itemView);
            label = (TextView) itemView.findViewById(R.id.textView);
            dateTime = (TextView) itemView.findViewById(R.id.textView2);
            chart = (LineChart) itemView.findViewById(R.id.chart1);
            image = (ImageView) itemView.findViewById(R.id.imgg);

            Log.i(LOG_TAG, "Adding Listener");
            itemView.setOnClickListener(this);
        }

        @Override
        public void onClick(View v) {
            myClickListener.onItemClick(getAdapterPosition(), v);
        }
    }

    public void setOnItemClickListener(MyClickListener myClickListener) {
        this.myClickListener = myClickListener;
    }

    public RecyclerViewAdapter(ArrayList<CardObject> myDataset) {
        mDataset = myDataset;
    }

    @Override
    public DataObjectHolder onCreateViewHolder(ViewGroup parent,
                                               int viewType) {
        View view = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.card_layout, parent, false);

        DataObjectHolder dataObjectHolder = new DataObjectHolder(view);
        return dataObjectHolder;
    }

    @Override
    public void onBindViewHolder(DataObjectHolder holder, final int position) {
        holder.label.setText(mDataset.get(position).getmText1());
        holder.dateTime.setText(mDataset.get(position).getmText2());

        if (mDataset.get(position).getmLineData() != null) {
            XAxis xAxis = holder.chart.getXAxis();
            xAxis.setLabelRotationAngle(-45);
            xAxis.setPosition(XAxis.XAxisPosition.BOTTOM);
            xAxis.setDrawGridLines(false);

            xAxis.setValueFormatter(new IAxisValueFormatter() {
                @Override
                public String getFormattedValue(float value, AxisBase axis) {
                    return mDataset.get(position).getmDateLabels().get((int) value);
                }
            });

            holder.chart.setData(mDataset.get(position).getmLineData());
            ((ViewManager)holder.image.getParent()).removeView(holder.image);
            holder.chart.invalidate();
        } else {
            ((ViewManager)holder.chart.getParent()).removeView(holder.chart);
            //holder.chart.setEnabled(false);

            if (mDataset.get(position).getmText2().endsWith(".jpg")) {

                String imgUrl = mDataset.get(position).getmText2();
                Picasso.with(holder.image.getContext()).load(imgUrl).fit().into(holder.image);

                ((ViewManager)holder.dateTime.getParent()).removeView(holder.dateTime);
            } else {
                ((ViewManager)holder.image.getParent()).removeView(holder.image);
            }
        }
    }

    public void addItem(CardObject dataObj, int index) {
        mDataset.add(index, dataObj);
        notifyItemInserted(index);
    }

    public void addItem(CardObject dataObj) {
        mDataset.add(dataObj);
        notifyItemInserted(mDataset.size() - 1);
    }

    public void deleteItem(int index) {
        mDataset.remove(index);
        notifyItemRemoved(index);
    }

    @Override
    public int getItemCount() {
        return mDataset.size();
    }

    public interface MyClickListener {
        public void onItemClick(int position, View v);
    }
}