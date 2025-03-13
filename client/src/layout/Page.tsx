export default function Page({ children }: React.PropsWithChildren) {
  return (
    <div className='flex flex-col min-h-screen '>
      <header></header>
      <main className="grow">{children}</main>
      <footer></footer>
    </div>
  );
}
