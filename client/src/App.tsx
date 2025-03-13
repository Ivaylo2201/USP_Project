import { BrowserRouter, Route, Routes } from 'react-router';
import PhonesPage from './pages/PhonesPage';

export default function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<></>} />
        <Route path='/phones' element={<PhonesPage />} />
      </Routes>
    </BrowserRouter>
  );
}
