import React from 'react';
import { Routes, Route } from 'react-router-dom';
import About from './components/About';
import Register from './components/Register';
import ShortUrlsTable from './components/ShortUrlTable';
import ShortUrlInfo from './components/ShortUrlInfo';
import Login from './components/Login';
import './App.css';

function App() {
    return (
        <div>
            <nav>
                <ul>
                    <li><a href="/">Home</a></li>
                    <li><a href="/about">About</a></li>
                    <li><a href="/short-urls">Short URLs</a></li>
                    <li><a href="/login">Login</a></li>
                    <li><a href="/register">Register</a></li>
                </ul>
            </nav>

            <Routes>
                <Route path="/" element={<h1>Welcome to the URL Shortener</h1>} />
                <Route path="/about" element={<About />} />
                <Route path="/short-urls" element={<ShortUrlsTable />} />
                <Route path="/short-urls/:id" element={<ShortUrlInfo />} />
                <Route path="/login" element={<Login />} />
                <Route path="/register" element={<Register />} />
            </Routes>
        </div>
    );
}

export default App;
