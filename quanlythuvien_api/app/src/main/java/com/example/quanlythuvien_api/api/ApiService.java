package com.example.quanlythuvien_api.api;

import com.example.quanlythuvien_api.model.User;
import com.google.gson.Gson;
import com.google.gson.GsonBuilder;

import java.util.List;

import retrofit2.Call;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;
import retrofit2.http.GET;
import retrofit2.http.Query;

public interface ApiService {


    Gson gson = new GsonBuilder().setDateFormat("dd-mm-yyy").create();

    ApiService apiService = new Retrofit.Builder()
            .baseUrl("https://my-json-server.typicode.com/doandang1501/qltv/")
            .addConverterFactory(GsonConverterFactory.create(gson))
            .build()
            .create(ApiService.class);

    @GET("posts")
    Call<List<User>> getListUsers(@Query("userId") int userId);
}
