import { BrowserRouter, Route, Routes } from 'react-router';
import PhonesPage from './pages/PhonesPage';
import HomePage from './pages/HomePage';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<HomePage />} />
        <Route path='/catalogue' element={<PhonesPage />} />
        {/* <Route path='/cart' element={<CartPage />} /> */}
      </Routes>
    </BrowserRouter>
  );
}
