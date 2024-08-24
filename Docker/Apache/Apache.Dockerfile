FROM httpd:2.4
#COPY ./public-html/ /usr/local/apache2/htdocs/

COPY ./httpd.conf /usr/local/apache2/conf/httpd.conf
COPY ./httpd-vhosts.conf /usr/local/apache2/conf/extra/httpd-vhosts.conf
COPY ./server.crt /usr/local/apache2/conf/extra
COPY ./server.key /usr/local/apache2/conf/extra