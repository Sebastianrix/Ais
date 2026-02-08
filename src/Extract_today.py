{
  "nbformat": 4,
  "nbformat_minor": 0,
  "metadata": {
    "colab": {
      "provenance": [],
      "authorship_tag": "ABX9TyNYR0+qbKvMx3Pfa7+WgSrR",
      "include_colab_link": true
    },
    "kernelspec": {
      "name": "python3",
      "display_name": "Python 3"
    },
    "language_info": {
      "name": "python"
    }
  },
  "cells": [
    {
      "cell_type": "markdown",
      "metadata": {
        "id": "view-in-github",
        "colab_type": "text"
      },
      "source": [
        "<a href=\"https://colab.research.google.com/github/Sebastianrix/Ais/blob/main/ais.ipynb\" target=\"_parent\"><img src=\"https://colab.research.google.com/assets/colab-badge.svg\" alt=\"Open In Colab\"/></a>"
      ]
    },
    {
      "cell_type": "code",
      "source": [
        "from datetime import datetime\n",
        "import pandas as pd\n",
        "import matplotlib\n",
        "import geopandas as gpd\n",
        "import numpy as np\n",
        "import matplotlib.pyplot as plt"
      ],
      "metadata": {
        "id": "b7OuI2o-ARZ5"
      },
      "execution_count": 2,
      "outputs": []
    },
    {
      "cell_type": "markdown",
      "source": [
        "URL for a given zip-data folder is routed with date as value, so we forge string with todays datetime"
      ],
      "metadata": {
        "id": "bBbWt5_EBGrE"
      }
    },
    {
      "cell_type": "code",
      "source": [
        "value_datetime = str(datetime.datetime.today()).split()[0]\n",
        "root_url = \"http://aisdata.ais.dk/\"\n",
        "\n",
        "half_string = \"aisdk-\"\n",
        "\n",
        "URL_RawData_Today = root_url + half_string + value_datetime\n",
        "\n",
        "\n",
        "print(URL_RawData_Today)"
      ],
      "metadata": {
        "colab": {
          "base_uri": "https://localhost:8080/"
        },
        "id": "XAb09ojS_ngK",
        "outputId": "7d68f389-ce0e-49f4-eb10-325bdfd844dd"
      },
      "execution_count": 7,
      "outputs": [
        {
          "output_type": "stream",
          "name": "stdout",
          "text": [
            "http://aisdata.ais.dk/aisdk-2026-02-08\n"
          ]
        }
      ]
    },
    {
      "cell_type": "code",
      "source": [],
      "metadata": {
        "colab": {
          "base_uri": "https://localhost:8080/"
        },
        "id": "B_JpEyIX_rtp",
        "outputId": "df5837ee-88ff-4593-aed0-ce84df192018"
      },
      "execution_count": 4,
      "outputs": [
        {
          "output_type": "stream",
          "name": "stdout",
          "text": [
            "2026-02-08\n"
          ]
        }
      ]
    },
    {
      "cell_type": "markdown",
      "source": [
        "italicised tColumns in *.csv file                   Format\n",
        "----------------------------------------------------------------------------------------------------------------------------------------------------\n",
        "1.    Timestamp                         Timestamp from the AIS basestation, format: 31/12/2015 23:59:59\n",
        "2.    Type of mobile                    Describes what type of target this message is received from (class A AIS Vessel, Class B AIS vessel, etc)\n",
        "3.    MMSI                              MMSI number of vessel\n",
        "4.    Latitude                          Latitude of message report (e.g. 57,8794)\n",
        "5.    Longitude                         Longitude of message report (e.g. 17,9125)\n",
        "6.    Navigational status               Navigational status from AIS message if available, e.g.: 'Engaged in fishing', 'Under way using engine', mv.\n",
        "7.    ROT                               Rot of turn from AIS message if available\n",
        "8.    SOG                               Speed over ground from AIS message if available\n",
        "9.    COG                               Course over ground from AIS message if available\n",
        "10.   Heading                           Heading from AIS message if available\n",
        "11.   IMO                               IMO number of the vessel\n",
        "12.   Callsign                          Callsign of the vessel\n",
        "13.   Name                              Name of the vessel\n",
        "14.   Ship type                         Describes the AIS ship type of this vessel\n",
        "15.   Cargo type                        Type of cargo from the AIS message\n",
        "16.   Width                             Width of the vessel\n",
        "17.   Length                            Lenght of the vessel\n",
        "18.   Type of position fixing device    Type of positional fixing device from the AIS message\n",
        "19.   Draught                           Draugth field from AIS message\n",
        "20.   Destination                       Destination from AIS message\n",
        "21.   ETA                               Estimated Time of Arrival, if available  \n",
        "22.   Data source type                  Data source type, e.g. AIS\n",
        "23.   Size A                            Length from GPS to the bow\n",
        "24.   Size B                            Length from GPS to the stern\n",
        "25.   Size C                            Length from GPS to starboard side\n",
        "26.   Size D                            Length from GPS to port sideext"
      ],
      "metadata": {
        "id": "BYDo9kjP-QT7"
      }
    }
  ]
}