type ChampionIconProps<T> = {
  id: T;
  name: string;
  img: string;
  size: number;
  selected?: boolean;
  disabled?: boolean;
  showBorder?: boolean;
  onClick?: (id: T) => void;
};

const borderRadius = 4;

function ChampionIcon<T>({
  id,
  name,
  img,
  size,
  selected,
  disabled = false,
  showBorder = true,
  onClick = () => {},
}: ChampionIconProps<T>) {
  const selectedEffect = (
    <view
      style={{
        width: `${size}px`,
        height: `${size}px`,
        border: showBorder ? "1.5px solid yellow" : "none",
        position: "absolute",
        left: 0,
        top: 0,
        borderRadius: `${borderRadius}%`,
        backgroundColor: showBorder ? "rgba(255,242,0,0.03)" : "unset",
      }}
    />
  );
  const disabledEffect = (
    <view
      style={{
        width: `${size}px`,
        height: `${size}px`,
        position: "absolute",
        left: 0,
        top: 0,
        borderRadius: `${borderRadius}%`,
        backgroundColor: "rgba(0,0,0,0.8)",
      }}
    />
  );
  return (
    <view style={{ width: size }} onClick={() => onClick(id)}>
      <view
        style={{
          width: `${size}px`,
          height: `${size}px`,
          overflow: "hidden",
          borderRadius: `${borderRadius + 0.5}%`,
        }}
      >
        {img && (
          <image style={{ width: "100%", height: "100%" }} source={img} />
        )}
      </view>
      {selected && selectedEffect}
      {disabled && disabledEffect}
      <text
        style={{
          textAlign: "center",
          marginTop: "0.1rem",
          fontSize: "0.8rem",
          color: selected ? "white" : "gray",
          whiteSpace: "nowrap",
        }}
      >
        {name}
      </text>
    </view>
  );
}

function Placeholder({ size = 64 }: { size?: number }) {
  return <ChampionIcon id={-1} img="" name="" size={size} />;
}

ChampionIcon.Placeholder = Placeholder;

export default ChampionIcon;
