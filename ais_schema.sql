--
-- PostgreSQL database dump
--

\restrict 5jKFr1PvZyOl4cAaRZXq0Fa72mBoF33y90Lx3kNtCzU2eu8gyXQgyKM8ZGk9kaC

-- Dumped from database version 18.4 (Ubuntu 18.4-1.pgdg24.04+1)
-- Dumped by pg_dump version 18.4 (Ubuntu 18.4-1.pgdg24.04+1)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Name: public; Type: SCHEMA; Schema: -; Owner: ais
--

-- *not* creating schema, since initdb creates it


ALTER SCHEMA public OWNER TO ais;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: anomaly_flags; Type: TABLE; Schema: public; Owner: ais
--

CREATE TABLE public.anomaly_flags (
    anomaly_flag_id bigint NOT NULL,
    tanker_id bigint,
    position_id bigint,
    staging_id bigint,
    anomaly_type_id bigint NOT NULL,
    source character varying(50) DEFAULT 'system'::character varying NOT NULL,
    confidence numeric(5,2) DEFAULT 1.00,
    notes text,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.anomaly_flags OWNER TO ais;

--
-- Name: anomaly_flags_anomaly_flag_id_seq; Type: SEQUENCE; Schema: public; Owner: ais
--

CREATE SEQUENCE public.anomaly_flags_anomaly_flag_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.anomaly_flags_anomaly_flag_id_seq OWNER TO ais;

--
-- Name: anomaly_flags_anomaly_flag_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: ais
--

ALTER SEQUENCE public.anomaly_flags_anomaly_flag_id_seq OWNED BY public.anomaly_flags.anomaly_flag_id;


--
-- Name: anomaly_types; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.anomaly_types (
    anomaly_type_id bigint NOT NULL,
    code character varying(100) NOT NULL,
    name character varying(255) NOT NULL,
    description text,
    severity character varying(30) NOT NULL,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.anomaly_types OWNER TO postgres;

--
-- Name: anomaly_types_anomaly_type_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.anomaly_types_anomaly_type_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.anomaly_types_anomaly_type_id_seq OWNER TO postgres;

--
-- Name: anomaly_types_anomaly_type_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.anomaly_types_anomaly_type_id_seq OWNED BY public.anomaly_types.anomaly_type_id;


--
-- Name: data_consumer_queue; Type: TABLE; Schema: public; Owner: ais
--

CREATE TABLE public.data_consumer_queue (
    queue_id bigint NOT NULL,
    source_batch_date date NOT NULL,
    priority integer NOT NULL,
    requester character varying(20) NOT NULL,
    status character varying(20) DEFAULT 'pending'::character varying NOT NULL,
    created_at timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public.data_consumer_queue OWNER TO ais;

--
-- Name: data_consumer_queue_queue_id_seq; Type: SEQUENCE; Schema: public; Owner: ais
--

CREATE SEQUENCE public.data_consumer_queue_queue_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.data_consumer_queue_queue_id_seq OWNER TO ais;

--
-- Name: data_consumer_queue_queue_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: ais
--

ALTER SEQUENCE public.data_consumer_queue_queue_id_seq OWNED BY public.data_consumer_queue.queue_id;


--
-- Name: data_date_archive; Type: TABLE; Schema: public; Owner: ais
--

CREATE TABLE public.data_date_archive (
    source_batch_date date NOT NULL,
    total_rows bigint NOT NULL,
    tanker_rows bigint NOT NULL,
    positions_inserted bigint NOT NULL,
    archived_at timestamp without time zone DEFAULT now() NOT NULL
);


ALTER TABLE public.data_date_archive OWNER TO ais;

--
-- Name: mmsi_country_codes; Type: TABLE; Schema: public; Owner: ais
--

CREATE TABLE public.mmsi_country_codes (
    mid_code character varying(3) NOT NULL,
    country_code character varying(2) NOT NULL,
    country_name character varying(100) NOT NULL,
    region character varying(50),
    created_at timestamp without time zone DEFAULT now()
);


ALTER TABLE public.mmsi_country_codes OWNER TO ais;

--
-- Name: tanker_positions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tanker_positions (
    position_id bigint NOT NULL,
    tanker_id bigint,
    voyage_id bigint,
    staging_id bigint,
    timestamp_utc timestamp without time zone NOT NULL,
    latitude double precision NOT NULL,
    longitude double precision NOT NULL,
    raw_imo character varying(20),
    imo_status character varying(20),
    raw_mmsi character varying(20),
    mmsi_status character varying(20),
    anomaly_flag boolean DEFAULT false,
    navigational_status character varying(100),
    rot double precision,
    sog double precision,
    cog double precision,
    heading double precision,
    draught double precision,
    destination character varying(255),
    eta timestamp without time zone,
    position_fixing_device character varying(100),
    data_source_type character varying(50),
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.tanker_positions OWNER TO postgres;

--
-- Name: tanker_positions_position_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tanker_positions_position_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tanker_positions_position_id_seq OWNER TO postgres;

--
-- Name: tanker_positions_position_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tanker_positions_position_id_seq OWNED BY public.tanker_positions.position_id;


--
-- Name: tanker_staging; Type: TABLE; Schema: public; Owner: ais
--

CREATE TABLE public.tanker_staging (
    staging_id bigint NOT NULL,
    timestamp_raw character varying(100),
    type_of_mobile character varying(100),
    mmsi character varying(20),
    latitude_raw character varying(50),
    longitude_raw character varying(50),
    navigational_status character varying(100),
    rot_raw character varying(50),
    sog_raw character varying(50),
    cog_raw character varying(50),
    heading_raw character varying(50),
    imo character varying(20),
    callsign character varying(50),
    vessel_name character varying(255),
    ship_type character varying(100),
    cargo_type character varying(100),
    width_raw character varying(50),
    length_raw character varying(50),
    position_fixing_device character varying(100),
    draught_raw character varying(50),
    destination character varying(255),
    eta_raw character varying(100),
    data_source_type character varying(50),
    size_a numeric(10,2),
    size_b numeric(10,2),
    size_c numeric(10,2),
    size_d numeric(10,2),
    source_file_name character varying(255),
    source_batch_date date,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    is_loaded boolean DEFAULT false
);


ALTER TABLE public.tanker_staging OWNER TO ais;

--
-- Name: tanker_staging_staging_id_seq; Type: SEQUENCE; Schema: public; Owner: ais
--

CREATE SEQUENCE public.tanker_staging_staging_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tanker_staging_staging_id_seq OWNER TO ais;

--
-- Name: tanker_staging_staging_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: ais
--

ALTER SEQUENCE public.tanker_staging_staging_id_seq OWNED BY public.tanker_staging.staging_id;


--
-- Name: tankers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tankers (
    tanker_id bigint NOT NULL,
    imo character varying(20),
    mmsi character varying(20),
    vessel_name character varying(255),
    callsign character varying(50),
    ship_type character varying(100),
    cargo_type character varying(100),
    type_of_mobile character varying(100),
    width numeric(10,2),
    length numeric(10,2),
    size_a numeric(10,2),
    size_b numeric(10,2),
    size_c numeric(10,2),
    size_d numeric(10,2),
    flag character varying(100),
    first_seen_at timestamp without time zone,
    last_seen_at timestamp without time zone,
    is_active boolean DEFAULT true,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.tankers OWNER TO postgres;

--
-- Name: tankers_tanker_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tankers_tanker_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tankers_tanker_id_seq OWNER TO postgres;

--
-- Name: tankers_tanker_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tankers_tanker_id_seq OWNED BY public.tankers.tanker_id;


--
-- Name: tracked_tankers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tracked_tankers (
    tracked_id bigint NOT NULL,
    imo character varying(20),
    mmsi character varying(20),
    source_trial character varying(100),
    notes text,
    is_active boolean DEFAULT false,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.tracked_tankers OWNER TO postgres;

--
-- Name: tracked_tankers_tracked_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tracked_tankers_tracked_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tracked_tankers_tracked_id_seq OWNER TO postgres;

--
-- Name: tracked_tankers_tracked_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tracked_tankers_tracked_id_seq OWNED BY public.tracked_tankers.tracked_id;


--
-- Name: voyages; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.voyages (
    voyage_id bigint NOT NULL,
    tanker_id bigint NOT NULL,
    voyage_status character varying(30) DEFAULT 'active'::character varying,
    start_time_utc timestamp without time zone NOT NULL,
    end_time_utc timestamp without time zone,
    start_position_id bigint,
    end_position_id bigint,
    start_port_name character varying(255),
    end_port_name character varying(255),
    destination_final character varying(255),
    eta_final timestamp without time zone,
    created_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP,
    updated_at timestamp without time zone DEFAULT CURRENT_TIMESTAMP
);


ALTER TABLE public.voyages OWNER TO postgres;

--
-- Name: voyages_voyage_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.voyages_voyage_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.voyages_voyage_id_seq OWNER TO postgres;

--
-- Name: voyages_voyage_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.voyages_voyage_id_seq OWNED BY public.voyages.voyage_id;


--
-- Name: anomaly_flags anomaly_flag_id; Type: DEFAULT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.anomaly_flags ALTER COLUMN anomaly_flag_id SET DEFAULT nextval('public.anomaly_flags_anomaly_flag_id_seq'::regclass);


--
-- Name: anomaly_types anomaly_type_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.anomaly_types ALTER COLUMN anomaly_type_id SET DEFAULT nextval('public.anomaly_types_anomaly_type_id_seq'::regclass);


--
-- Name: data_consumer_queue queue_id; Type: DEFAULT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.data_consumer_queue ALTER COLUMN queue_id SET DEFAULT nextval('public.data_consumer_queue_queue_id_seq'::regclass);


--
-- Name: tanker_positions position_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tanker_positions ALTER COLUMN position_id SET DEFAULT nextval('public.tanker_positions_position_id_seq'::regclass);


--
-- Name: tanker_staging staging_id; Type: DEFAULT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.tanker_staging ALTER COLUMN staging_id SET DEFAULT nextval('public.tanker_staging_staging_id_seq'::regclass);


--
-- Name: tankers tanker_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tankers ALTER COLUMN tanker_id SET DEFAULT nextval('public.tankers_tanker_id_seq'::regclass);


--
-- Name: tracked_tankers tracked_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tracked_tankers ALTER COLUMN tracked_id SET DEFAULT nextval('public.tracked_tankers_tracked_id_seq'::regclass);


--
-- Name: voyages voyage_id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.voyages ALTER COLUMN voyage_id SET DEFAULT nextval('public.voyages_voyage_id_seq'::regclass);


--
-- Name: anomaly_flags anomaly_flags_pkey; Type: CONSTRAINT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.anomaly_flags
    ADD CONSTRAINT anomaly_flags_pkey PRIMARY KEY (anomaly_flag_id);


--
-- Name: anomaly_types anomaly_types_code_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.anomaly_types
    ADD CONSTRAINT anomaly_types_code_key UNIQUE (code);


--
-- Name: anomaly_types anomaly_types_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.anomaly_types
    ADD CONSTRAINT anomaly_types_pkey PRIMARY KEY (anomaly_type_id);


--
-- Name: data_consumer_queue data_consumer_queue_pkey; Type: CONSTRAINT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.data_consumer_queue
    ADD CONSTRAINT data_consumer_queue_pkey PRIMARY KEY (queue_id);


--
-- Name: data_date_archive data_date_archive_pkey; Type: CONSTRAINT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.data_date_archive
    ADD CONSTRAINT data_date_archive_pkey PRIMARY KEY (source_batch_date);


--
-- Name: mmsi_country_codes mmsi_country_codes_pkey; Type: CONSTRAINT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.mmsi_country_codes
    ADD CONSTRAINT mmsi_country_codes_pkey PRIMARY KEY (mid_code);


--
-- Name: tanker_positions tanker_positions_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tanker_positions
    ADD CONSTRAINT tanker_positions_pkey PRIMARY KEY (position_id);


--
-- Name: tanker_staging tanker_staging_pkey; Type: CONSTRAINT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.tanker_staging
    ADD CONSTRAINT tanker_staging_pkey PRIMARY KEY (staging_id);


--
-- Name: tankers tankers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tankers
    ADD CONSTRAINT tankers_pkey PRIMARY KEY (tanker_id);


--
-- Name: tracked_tankers tracked_tankers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tracked_tankers
    ADD CONSTRAINT tracked_tankers_pkey PRIMARY KEY (tracked_id);


--
-- Name: tankers uq_tankers_imo; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tankers
    ADD CONSTRAINT uq_tankers_imo UNIQUE (imo);


--
-- Name: voyages voyages_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.voyages
    ADD CONSTRAINT voyages_pkey PRIMARY KEY (voyage_id);


--
-- Name: idx_anomaly_flags_position_id; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_anomaly_flags_position_id ON public.anomaly_flags USING btree (position_id);


--
-- Name: idx_anomaly_flags_tanker_id; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_anomaly_flags_tanker_id ON public.anomaly_flags USING btree (tanker_id);


--
-- Name: idx_anomaly_flags_type_id; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_anomaly_flags_type_id ON public.anomaly_flags USING btree (anomaly_type_id);


--
-- Name: idx_mmsi_country_mid; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_mmsi_country_mid ON public.mmsi_country_codes USING btree (mid_code);


--
-- Name: idx_staging_batch; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_staging_batch ON public.tanker_staging USING btree (source_batch_date);


--
-- Name: idx_staging_imo_unloaded; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_staging_imo_unloaded ON public.tanker_staging USING btree (imo) WHERE (is_loaded = false);


--
-- Name: idx_staging_shiptype_unloaded; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_staging_shiptype_unloaded ON public.tanker_staging USING btree (lower(TRIM(BOTH FROM ship_type))) WHERE (is_loaded = false);


--
-- Name: idx_tanker_positions_anomaly_flag; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tanker_positions_anomaly_flag ON public.tanker_positions USING btree (anomaly_flag);


--
-- Name: idx_tanker_positions_imo_status; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tanker_positions_imo_status ON public.tanker_positions USING btree (imo_status);


--
-- Name: idx_tanker_positions_raw_imo; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tanker_positions_raw_imo ON public.tanker_positions USING btree (raw_imo);


--
-- Name: idx_tanker_positions_staging_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tanker_positions_staging_id ON public.tanker_positions USING btree (staging_id);


--
-- Name: idx_tanker_positions_tanker_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tanker_positions_tanker_id ON public.tanker_positions USING btree (tanker_id);


--
-- Name: idx_tanker_positions_timestamp; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tanker_positions_timestamp ON public.tanker_positions USING btree (timestamp_utc);


--
-- Name: idx_tanker_positions_voyage_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tanker_positions_voyage_id ON public.tanker_positions USING btree (voyage_id);


--
-- Name: idx_tanker_staging_ship_type; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_tanker_staging_ship_type ON public.tanker_staging USING btree (lower(TRIM(BOTH FROM ship_type)));


--
-- Name: idx_tanker_staging_staging_id_desc; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_tanker_staging_staging_id_desc ON public.tanker_staging USING btree (staging_id DESC);


--
-- Name: idx_tanker_staging_unloaded; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_tanker_staging_unloaded ON public.tanker_staging USING btree (staging_id) WHERE (is_loaded = false);


--
-- Name: idx_tanker_timestamp_raw; Type: INDEX; Schema: public; Owner: ais
--

CREATE INDEX idx_tanker_timestamp_raw ON public.tanker_staging USING btree (timestamp_raw DESC);


--
-- Name: idx_tankers_flag; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tankers_flag ON public.tankers USING btree (flag);


--
-- Name: idx_tankers_imo; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tankers_imo ON public.tankers USING btree (imo);


--
-- Name: idx_tankers_mmsi; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tankers_mmsi ON public.tankers USING btree (mmsi);


--
-- Name: idx_tracked_tankers_imo; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_tracked_tankers_imo ON public.tracked_tankers USING btree (imo);


--
-- Name: idx_voyages_end_time; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_voyages_end_time ON public.voyages USING btree (end_time_utc);


--
-- Name: idx_voyages_start_time; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_voyages_start_time ON public.voyages USING btree (start_time_utc);


--
-- Name: idx_voyages_tanker_id; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX idx_voyages_tanker_id ON public.voyages USING btree (tanker_id);


--
-- Name: uq_anomaly_flags_position_type_source; Type: INDEX; Schema: public; Owner: ais
--

CREATE UNIQUE INDEX uq_anomaly_flags_position_type_source ON public.anomaly_flags USING btree (position_id, anomaly_type_id, source) WHERE (position_id IS NOT NULL);


--
-- Name: uq_tanker_position_known; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX uq_tanker_position_known ON public.tanker_positions USING btree (tanker_id, timestamp_utc, latitude, longitude) WHERE (tanker_id IS NOT NULL);


--
-- Name: anomaly_flags fk_anomaly_flags_position; Type: FK CONSTRAINT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.anomaly_flags
    ADD CONSTRAINT fk_anomaly_flags_position FOREIGN KEY (position_id) REFERENCES public.tanker_positions(position_id) ON DELETE CASCADE;


--
-- Name: anomaly_flags fk_anomaly_flags_staging; Type: FK CONSTRAINT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.anomaly_flags
    ADD CONSTRAINT fk_anomaly_flags_staging FOREIGN KEY (staging_id) REFERENCES public.tanker_staging(staging_id) ON DELETE SET NULL;


--
-- Name: anomaly_flags fk_anomaly_flags_tanker; Type: FK CONSTRAINT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.anomaly_flags
    ADD CONSTRAINT fk_anomaly_flags_tanker FOREIGN KEY (tanker_id) REFERENCES public.tankers(tanker_id) ON DELETE CASCADE;


--
-- Name: anomaly_flags fk_anomaly_flags_type; Type: FK CONSTRAINT; Schema: public; Owner: ais
--

ALTER TABLE ONLY public.anomaly_flags
    ADD CONSTRAINT fk_anomaly_flags_type FOREIGN KEY (anomaly_type_id) REFERENCES public.anomaly_types(anomaly_type_id) ON DELETE CASCADE;


--
-- Name: tanker_positions fk_positions_staging; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tanker_positions
    ADD CONSTRAINT fk_positions_staging FOREIGN KEY (staging_id) REFERENCES public.tanker_staging(staging_id) ON DELETE SET NULL;


--
-- Name: tanker_positions fk_positions_tanker; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tanker_positions
    ADD CONSTRAINT fk_positions_tanker FOREIGN KEY (tanker_id) REFERENCES public.tankers(tanker_id) ON DELETE CASCADE;


--
-- Name: tanker_positions fk_positions_voyage; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tanker_positions
    ADD CONSTRAINT fk_positions_voyage FOREIGN KEY (voyage_id) REFERENCES public.voyages(voyage_id) ON DELETE SET NULL;


--
-- Name: voyages fk_voyages_tanker; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.voyages
    ADD CONSTRAINT fk_voyages_tanker FOREIGN KEY (tanker_id) REFERENCES public.tankers(tanker_id) ON DELETE CASCADE;


--
-- Name: TABLE anomaly_types; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.anomaly_types TO ais;


--
-- Name: SEQUENCE anomaly_types_anomaly_type_id_seq; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public.anomaly_types_anomaly_type_id_seq TO ais;


--
-- Name: TABLE tanker_positions; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.tanker_positions TO ais;


--
-- Name: SEQUENCE tanker_positions_position_id_seq; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public.tanker_positions_position_id_seq TO ais;


--
-- Name: TABLE tankers; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.tankers TO ais;


--
-- Name: SEQUENCE tankers_tanker_id_seq; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public.tankers_tanker_id_seq TO ais;


--
-- Name: TABLE tracked_tankers; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.tracked_tankers TO ais;


--
-- Name: SEQUENCE tracked_tankers_tracked_id_seq; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public.tracked_tankers_tracked_id_seq TO ais;


--
-- Name: TABLE voyages; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON TABLE public.voyages TO ais;


--
-- Name: SEQUENCE voyages_voyage_id_seq; Type: ACL; Schema: public; Owner: postgres
--

GRANT ALL ON SEQUENCE public.voyages_voyage_id_seq TO ais;


--
-- PostgreSQL database dump complete
--

\unrestrict 5jKFr1PvZyOl4cAaRZXq0Fa72mBoF33y90Lx3kNtCzU2eu8gyXQgyKM8ZGk9kaC

