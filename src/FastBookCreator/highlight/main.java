AlertDialog.Builder builder = new AlertDialog.Builder(context);
            View view = layoutInflater.inflate(R.layout.meme_post_actions_dialog,null);
            builder.setView(view);
int width = (int)(getResources().getDisplayMetrics().widthPixels*0.50); //<-- int width=400;
int height = (int)(getResources().getDisplayMetrics().heightPixels*0.50);//<-- int height =300;
AlertDialog alertDialog = builder.create();
alertDialog.getWindow().setLayout(width, height);
