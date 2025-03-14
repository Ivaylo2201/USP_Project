import { ShoppingCart } from 'lucide-react';
import { Link } from 'react-router';

type CartButtonProps = {
  items: number;
  total: number;
};

export default function CartButton({ items, total }: CartButtonProps) {
  return (
    <Link
      to='/cart'
      className='flex justify-center items-center rounded-full p-2 gap-4 cursor-pointer relative font-dmsans'
    >
      <div className='relative'>
        <ShoppingCart strokeWidth={1.75} />
        <span className='size-4.5 text-white flex justify-center items-center text-xs absolute -top-3 -right-3 rounded-full bg-blue-400 p-2'>
          {items}
        </span>
      </div>
      <p className='text-darkblue'>${total.toFixed(2)}</p>
    </Link>
  );
}
