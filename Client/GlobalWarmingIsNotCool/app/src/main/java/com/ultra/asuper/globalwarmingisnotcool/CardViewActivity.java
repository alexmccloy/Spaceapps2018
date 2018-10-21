package com.ultra.asuper.globalwarmingisnotcool;

import javax.xml.xpath.XPath;
import javax.xml.xpath.XPathConstants;
import javax.xml.xpath.XPathExpressionException;
import javax.xml.xpath.XPathFactory;

import android.Manifest;
import android.app.DownloadManager;
import android.content.pm.PackageManager;
import android.graphics.Color;
import android.location.Location;
import android.os.Bundle;
import android.support.design.widget.FloatingActionButton;
import android.support.design.widget.Snackbar;
import android.support.v4.app.ActivityCompat;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.util.Log;
import android.view.View;
import android.view.Menu;
import android.view.MenuItem;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.Volley;
import com.github.mikephil.charting.charts.LineChart;
import com.github.mikephil.charting.components.XAxis;
import com.github.mikephil.charting.data.Entry;
import com.github.mikephil.charting.data.LineData;
import com.github.mikephil.charting.data.LineDataSet;
import com.android.volley.toolbox.StringRequest;
import com.google.android.gms.location.FusedLocationProviderClient;
import com.google.android.gms.location.LocationServices;
import com.google.android.gms.tasks.OnSuccessListener;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

public class CardViewActivity extends AppCompatActivity {

    private RecyclerView mRecyclerView;
    private RecyclerView.Adapter mAdapter;
    private RecyclerView.LayoutManager mLayoutManager;
    private FusedLocationProviderClient mFusedLocationClient;

    private static String LOG_TAG = "CardViewActivity";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_card_view);

        mRecyclerView = (RecyclerView) findViewById(R.id.my_recycler_view);
        mRecyclerView.setHasFixedSize(true);
        mLayoutManager = new LinearLayoutManager(this);
        mRecyclerView.setLayoutManager(mLayoutManager);
        mAdapter = new RecyclerViewAdapter(new ArrayList<CardObject>());
        mFusedLocationClient = LocationServices.getFusedLocationProviderClient(this);


        if (ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this, Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            // TODO: Consider calling
            //    ActivityCompat#requestPermissions
            // here to request the missing permissions, and then overriding
            //   public void onRequestPermissionsResult(int requestCode, String[] permissions,
            //                                          int[] grantResults)
            // to handle the case where the user grants the permission. See the documentation
            // for ActivityCompat#requestPermissions for more details.
            return;
        }

        final RequestQueue queue = Volley.newRequestQueue(this);
        mFusedLocationClient.getLastLocation()
                .addOnSuccessListener(this, new OnSuccessListener<Location>() {
                    @Override
                    public void onSuccess(Location loc) {
                        // Got last known location. In some rare situations this can be null.
                        if (loc != null) {
                            String url = "http://yourboythewebapp.azurewebsites.net/api/Values?";
                            url += "lat=" + loc.getLatitude() + "&lon=" + loc.getLongitude();
                            StringRequest req = new StringRequest(Request.Method.GET, url,
                                    new Response.Listener<String>() {
                                        @Override
                                        public void onResponse(String response) {
                                            XPathFactory factory = XPathFactory.newInstance();
                                            XPath xPath = factory.newXPath();
                                            try {
                                                JSONArray json = new JSONArray(response);

                                                for (int i = 0; i < json.length(); i++) {
                                                    JSONObject jsonObj = json.getJSONObject(i);
                                                    CardObject obj;
                                                    if (jsonObj.getString("DataType").startsWith("temperature")) {
                                                        obj = getTempCardFromJSON(jsonObj);
                                                        ((RecyclerViewAdapter) mAdapter).addItem(obj);
                                                    } else if (jsonObj.getString("DataType").startsWith("rain")) {
//                                                        obj = getRainCardFromJSON(jsonObj);
//                                                        ((RecyclerViewAdapter) mAdapter).addItem(obj);
                                                    }

                                                }

                                                mRecyclerView.setAdapter(mAdapter);

                                            } catch (org.json.JSONException e) {
                                                e.printStackTrace();
                                            }
                                        }
                                    },
                                    new Response.ErrorListener() {
                                        @Override
                                        public void onErrorResponse(VolleyError error) {
                                            // handle error response
                                        }
                                    }
                            );
                            queue.add(req);
                        }
                    }
                });

        // Code to Add an item with default animation
        //((MyRecyclerViewAdapter) mAdapter).addItem(obj, index);

        // Code to remove an item with default animation
        //((MyRecyclerViewAdapter) mAdapter).deleteItem(index);
    }

    private CardObject getTempCardFromJSON(JSONObject jsonObj) throws JSONException {
        String city = jsonObj.getString("City");

        List<Entry> entries = new ArrayList<Entry>();
        List<String> xDateLabels = new ArrayList<String>();
        JSONObject data = jsonObj.getJSONObject("Data");
        int index = 0;
        for (Iterator<String> it = data.keys(); it.hasNext(); ) {
            String key = it.next();
            float val = (float)((double)data.get(key));
            entries.add(new Entry(index++, val));
            xDateLabels.add(key.substring(0,4));
        }
        LineDataSet dataSet = new LineDataSet(entries, "");

        dataSet.setColor(Color.rgb(255, 0, 0));
        dataSet.setValueTextColor(Color.rgb(0, 255, 0));
        LineData lineData = new LineData(dataSet);

        CardObject obj = new CardObject(jsonObj.getString("CardTitle"),
                "City: " + city, lineData, xDateLabels);

        return obj;
    }


    private CardObject getRainCardFromJSON(JSONObject jsonObj) throws JSONException {
        String city = jsonObj.getString("City");

        List<Entry> entries = new ArrayList<Entry>();
        List<String> xDateLabels = new ArrayList<String>();
        JSONObject data = jsonObj.getJSONObject("Data");
        int index = 0;
        for (Iterator<String> it = data.keys(); it.hasNext(); ) {
            String key = it.next();
            float val = (float)((double)data.get(key));
            entries.add(new Entry(index++, val));
            xDateLabels.add(key.substring(0,4));
        }
        LineDataSet dataSet = new LineDataSet(entries, "");

        dataSet.setColor(Color.rgb(255, 0, 0));
        dataSet.setValueTextColor(Color.rgb(0, 255, 0));
        LineData lineData = new LineData(dataSet);

        CardObject obj = new CardObject(jsonObj.getString("CardTitle"),
                "City: " + city, lineData, xDateLabels);

        return obj;
    }

    @Override
    protected void onResume() {
        super.onResume();
        ((RecyclerViewAdapter) mAdapter).setOnItemClickListener(new RecyclerViewAdapter
                .MyClickListener() {
            @Override
            public void onItemClick(int position, View v) {
                Log.i(LOG_TAG, " Clicked on Item " + position);
            }
        });
    }

//    private ArrayList<CardObject> getData() {
//        ArrayList results = new ArrayList<CardObject>();
//        for (int index = 0; index < 20; index++) {
//            List<Entry> entries = new ArrayList<Entry>();
//            for (int i = 0; i < 50; i++) {
//                entries.add(new Entry(i, i % 5));
//            }
//            LineDataSet dataSet = new LineDataSet(entries, "Label");
//            dataSet.setColor(Color.rgb(255,0,0));
//            dataSet.setValueTextColor(Color.rgb(0,255,0));
//            LineData lineData = new LineData(dataSet);
//
//            CardObject obj = new CardObject("Some Primary Text " + index,
//                    "Secondary " + index, lineData);
//            results.add(index, obj);
//        }
//        return results;
//    }
}
