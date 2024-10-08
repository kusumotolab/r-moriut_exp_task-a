# ベースイメージを指定
FROM ubuntu:24.04

# 必要なパッケージをインストール
RUN apt-get update \
    && apt-get install -y \
    nginx \
    openjdk-8-jdk \
    unzip \
    wget \
    gfortran

# nginxの設定ファイルとWebリソース，ゲームの開発プロジェクトをコンテナにコピー
COPY . .
RUN cp /conf/default.conf /etc/nginx/conf.d/default.conf \
    && cp -r /web/* /var/www/html

# Gradleのインストール
RUN wget https://services.gradle.org/distributions/gradle-7.6-bin.zip -P /tmp \
    && unzip -d /opt/gradle /tmp/gradle-7.6-bin.zip \
    && ln -s /opt/gradle/gradle-7.6 /opt/gradle/latest

# Gradleのバイナリをパスに追加
ENV PATH=/opt/gradle/latest/bin:${PATH}

# Gradleを使用してプロジェクトをビルド
RUN cd game_project \
    && gradle build

# ゲームの実行ファイルを移動
RUN mkdir -p /var/www/html/publish \
    && mv game_project/build/libs/game.jar /var/www/html/publish

# ポートの指定
EXPOSE 80

# nginxの起動
# フォアグラウンドで動くように設定（nginxはデフォルトでデーモンとして動くため）
CMD ["nginx", "-g", "daemon off;"]