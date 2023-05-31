package com.example.quanlythuvien_api.adapter;

import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import androidx.annotation.NonNull;
import androidx.recyclerview.widget.RecyclerView;

import com.example.quanlythuvien_api.R;
import com.example.quanlythuvien_api.model.User;

import java.util.ArrayList;
import java.util.List;

public class UserAdapter extends RecyclerView.Adapter<UserAdapter.UserViewHolder> {

    private List<User> mListUser;

    public UserAdapter(List<User> mListUser) {
        this.mListUser = mListUser;
    }

    @NonNull
    @Override
    public UserViewHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.item_user,parent,false);
        return new UserViewHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull UserViewHolder holder, int position) {
        User user = mListUser.get(position);
        if (user == null){
            return;
        }
        holder.tvma.setText(user.getMa_loaisach());
        holder.tvten.setText(user.getTen_loaisach());
    }

    @Override
    public int getItemCount() {
        if (mListUser != null){
            return mListUser.size();
        }
        return 0;
    }



    public static class UserViewHolder extends RecyclerView.ViewHolder {

        private final TextView tvma, tvten;

        public UserViewHolder(@NonNull View itemView) {
            super(itemView);
            tvma = itemView.findViewById(R.id.tv_ma);
            tvten = itemView.findViewById(R.id.tv_ten);
        }
    }


}
