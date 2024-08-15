import styles from "./index.module.scss";

type AvatarProps = {
  name?: string;
  level?: number;
  avatar: string;
  size?: number;
};

function Avatar({ name, level, avatar, size = 64 }: AvatarProps) {
  return (
    <view className={styles.avatar}>
      <view
        style={{
          width: `${size}px`,
          height: `${size}px`,
          overflow: "hidden",
        }}
      >
        <image src={avatar} />
      </view>
      {name ? (
        <view
          style={{ display: "flex", marginLeft: "10px", fontSize: "0.75rem" }}
        >
          <view>
            <text style={{ color: "#FEFFFF" }}>{name}</text>
          </view>
          <view>
            <text
              style={{ color: "#D9EAF2", fontSize: "0.625rem" }}
            >{`Lv.<b>${level}</b>`}</text>
          </view>
        </view>
      ) : null}
    </view>
  );
}

export default Avatar;
